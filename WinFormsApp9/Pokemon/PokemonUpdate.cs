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
    
    public partial class PokemonUpdate : Form
    {
        private PokemonDto _pokemonToUpdate;
        public PokemonUpdate(PokemonDto pokemon)
        {
            InitializeComponent();
            _pokemonToUpdate = pokemon;
            this.Load += PokemonUpdate_Load;
            

        }

        private async void PokemonUpdate_Load(object sender, EventArgs e)
        {

            textBox1.Text = _pokemonToUpdate.Name;
            textBox2.Text = _pokemonToUpdate.BirthDate.ToString("yyyy.MM.dd");

        }



        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("İsim boş olamaz.");
                return;
            }

            // Tarih kontrolü ve dönüşümü
            if (!DateTime.TryParseExact(textBox2.Text, "yyyy.MM.dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                MessageBox.Show("Tarih geçerli değil. Lütfen yyyy.MM.dd formatında giriniz.");
                return;
            }

            _pokemonToUpdate.Name = textBox1.Text.Trim();
            _pokemonToUpdate.BirthDate = birthDate;

            try
            {
                using HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(_pokemonToUpdate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string url = $"https://localhost:7170/api/Pokemon/{_pokemonToUpdate.Id}";

                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pokemon başarıyla güncellendi.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Güncelleme sırasında hata oluştu: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
