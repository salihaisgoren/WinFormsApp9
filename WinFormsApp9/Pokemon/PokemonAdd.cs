using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp9.Models;

namespace WinFormsApp9.Pokemon
{
    public partial class PokemonAdd : Form
    {
        public PokemonAdd()
        {
            InitializeComponent();
            this.Load += PokemonAdd_Load;
        }

        private async void PokemonAdd_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadOwnersAsync();

        }

        private async Task LoadCategoriesAsync()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => true;

            using (HttpClient client = new HttpClient(handler))
            {
                var response = await client.GetAsync("https://localhost:7170/api/Category");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(json);

                    cmbCategory.DataSource = categories;
                    cmbCategory.DisplayMember = "Name";
                    cmbCategory.ValueMember = "Id";
                }
            }
        }

        private async Task LoadOwnersAsync()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => true;

            using (HttpClient client = new HttpClient(handler))
            {
                var response = await client.GetAsync("https://localhost:7170/api/Owner");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var owners = JsonConvert.DeserializeObject<List<OwnerDto>>(json);

                    cmbOwner.DataSource = owners;
                    cmbOwner.DisplayMember = "FirstName";
                    cmbOwner.ValueMember = "Id";
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Ad alanı boş mu kontrolü
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Ad alanı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Doğum tarihi doğru formatta mı? (yyyy-MM-dd)
                string dateInput = textBox2.Text.Trim();
                if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    MessageBox.Show("Doğum tarihini 'yyyy-MM-dd' formatında giriniz. Örnek: 2025-07-27", "Geçersiz Tarih", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ComboBox kontrolleri
                if (cmbCategory.SelectedValue == null || !(cmbCategory.SelectedValue is int))
                {
                    MessageBox.Show("Lütfen bir kategori seçiniz.", "Kategori Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbOwner.SelectedValue == null || !(cmbOwner.SelectedValue is int))
                {
                    MessageBox.Show("Lütfen bir owner seçiniz.", "Owner Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedCategoryId = (int)cmbCategory.SelectedValue;
                int selectedOwnerId = (int)cmbOwner.SelectedValue;

                var newPokemon = new PokemonDto
                {
                    Name = textBox1.Text.Trim(),
                    BirthDate = parsedDate,
                   
                };

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => true;

                using (HttpClient client = new HttpClient(handler))
                {
                    string json = JsonConvert.SerializeObject(newPokemon);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                  
                    var url = $"https://localhost:7170/api/Pokemon?ownerId={selectedOwnerId}&categoryId={selectedCategoryId}";

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Pokemon başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Hata: {response.StatusCode}\nSunucu cevabı: {responseBody}", "Sunucu Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "İstisna", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
