

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp9.Models;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WinFormsApp9.Pokemon
{
    public partial class UIPokemonList : Form
    {
        public UIPokemonList()
        {
            InitializeComponent();
            this.Load += UIPokemonList_Load;
        }
        private async void UIPokemonList_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => true;

                using (HttpClient client = new HttpClient(handler))
                {
                    string url = "https://localhost:7170/api/Pokemon";

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var pokemons = JsonConvert.DeserializeObject<List<PokemonDto>>(json);


                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = pokemons;
                    }
                    else
                    {
                        MessageBox.Show($"API Hatası: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private async void button5_Click(object sender, EventArgs e) //Yenile
        {
            await LoadDataAsync();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void button1_Click(object sender, EventArgs e) //Add
        {
            PokemonAdd listForm = new PokemonAdd();
            listForm.Show();
        }

        private async void button2_Click(object sender, EventArgs e) //Delete
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek bir Pokemon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow.Cells["Id"].Value is int pokemonId)
            {
                var confirm = MessageBox.Show($"Seçilen Pokemon'u silmek istediğinizden emin misiniz? (ID: {pokemonId})",
                                              "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        var handler = new HttpClientHandler();
                        handler.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => true;

                        using (HttpClient client = new HttpClient(handler))
                        {
                            string url = $"https://localhost:7170/api/Pokemon/{pokemonId}";

                            var response = await client.DeleteAsync(url);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Pokemon başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadDataAsync(); // Listeyi yenile
                            }
                            else
                            {
                                MessageBox.Show($"Silme işlemi başarısız: {response.StatusCode}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir Pokemon seçilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)  //Update
        {
            
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            var selectedRow = dataGridView1.SelectedRows[0];

            if (selectedRow.Cells["Id"].Value == null)
            {
                MessageBox.Show("Seçilen satır geçersiz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pokemon = new PokemonDto
            {
                Id = Convert.ToInt32(selectedRow.Cells["Id"].Value),
                Name = selectedRow.Cells["Name"].Value.ToString(),
                BirthDate = Convert.ToDateTime(selectedRow.Cells["BirthDate"].Value)
            };

            var updateForm = new PokemonUpdate(pokemon);
            updateForm.ShowDialog();
        }




        private void button4_Click(object sender, EventArgs e) //Detail
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Detay için satır seçiniz.");
                return;
            }

            var selected = dataGridView1.SelectedRows[0].DataBoundItem;
            var json = JsonSerializer.Serialize(selected, new System.Text.Json. JsonSerializerOptions { WriteIndented = true });
            MessageBox.Show(json, "Detay");
        }
    }
}
