using LashaMurgvaLominadzeShraieri.Quiz3.Models;
using LashaMurgvaLominadzeShraieri.Quiz3.Services;
using System.ComponentModel;

namespace LashaMurgvaLominadzeShraieri.Quiz3
{
    public partial class Form1 : Form
    {
        private readonly PersonService _personService;
        private readonly BindingList<Person> _bindingList;
        private int _currentEditIndex = -1;

        public Form1()
        {
            InitializeComponent();
            _personService = new PersonService();
            _bindingList = new BindingList<Person>();
            listBox.DataSource = _bindingList;
            listBox.DisplayMember = "ToString"; // Display the ToString() output in the ListBox
        }

        // Add Button Click Event
        private void addBtn_Click(object sender, EventArgs e)
        {
            UserDialog dialog = new UserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var person = new Person(
                    dialog.Name,
                    dialog.Lastname,
                    dialog.Email,
                    dialog.Gender,
                    dialog.Age
                );

                _personService.AddPerson(person);
                Sync(); // Update the list
            }
        }

        // Edit Button Click Event
        private void editBtn_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index != -1)
            {
                var selected = _personService.GetPeople().ToList()[index];
                UserDialog dialog = new UserDialog(selected);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selected.Name = dialog.Name;
                    selected.Lastname = dialog.Lastname;
                    selected.Email = dialog.Email;
                    selected.Gender = dialog.Gender;
                    selected.Age = dialog.Age;

                    _personService.UpdatePerson(index, selected);
                    Sync(); // Update the list
                }
            }
        }

        // Delete Button Click Event
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index != -1)
            {
                _personService.DeletePerson(index);
                Sync(); // Update the list
            }
        }

        // Sync the list with updated data
        private void Sync()
        {
            _bindingList.Clear(); // Clear current list
            var people = _personService.GetPeople(); // Get updated people from service
            foreach (var person in people)
            {
                _bindingList.Add(person); // Add each person to the BindingList
            }
        }
    }
}
