using WinFormsApp9.Pokemon;

namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)  //Pokemon
        {
            UIPokemonList listForm = new UIPokemonList();
            listForm.Show();
        }
    }
}
