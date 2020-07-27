using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TODO.Mappings;
using TODO.Entities;
using NHibernate.Linq;


namespace TODO
{
    public partial class MainWindow : Form
    {
        List<TaskToDo> toDoList;  
        int mode = 0; // 1 - tryb dodawania, 2 - tryb edycji

        public MainWindow()
        {
            InitializeComponent();
            editTextBox.Visible = false;

            /***********************************************************POBIERANIE LIST Z ZADANIAMI NA PODSTAWIE DATY*************************************************************/

            //pobranie z bazy listy zadań na dany dzień na podstawie daty w której została uruchomiana aplikacja
            toDoList = DataBaseSession.getListByDate((DateTime.Now.ToShortDateString()));
            foreach (TaskToDo task in toDoList)
            {
                int x = tasksList.Items.Add(task.Description);
                if (task.Status == 1) tasksList.SetItemChecked(x, true);
                else tasksList.SetItemChecked(x, false);
            }

            //powiadomienia na dziś po włączeniu aplikacji: liczba pozostałych do wykonania zadań
            int tasksCount = tasksList.Items.Count;
            int tasksChecked = tasksList.CheckedItems.Count;
            int tasksToDo = tasksCount - tasksChecked;

            if(tasksToDo != 0)
            {
                MessageBox.Show("Masz dziś niewykonane zadania! Liczba pozostałych zadań: " + tasksToDo, "Uwaga!");
            }
            else
            {
                MessageBox.Show("Wszystkie zadania z listy na dziś zostały wykonane :)", "Gratulacje!");
            }

        }

        //Reakcja na zdarzenie zmiany daty w kalendarzu - aktualnie widoczna lista jest czyszczona i wypełniana "zdarzeniami" które są zaplanowane na zaznaczoną datę 
        private void TaskCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            editTextBox.Visible = false;
            editTextBox.Text = "";
            mode = 0;
            tasksList.Items.Clear();
            toDoList = DataBaseSession.getListByDate(taskCalendar.SelectionRange.Start.ToShortDateString());
            foreach (TaskToDo task in toDoList)
            {
                int x = tasksList.Items.Add(task.Description);
                if (task.Status == 1) tasksList.SetItemChecked(x, true);
                else tasksList.SetItemChecked(x, false);
            }
        }
        /*************************************************************************************************************************************************************************/



        /*************************************************************************OBSŁUGA PRZYCISKÓW******************************************************************************/
        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            mode = 1;
            tasksList.Enabled = false;
            editTextBox.Visible = true;
            editTextBox.Focus();
            editTextBox.Text = "";
        }
      
        private void EditButton_Click(object sender, EventArgs e)
        {
            tasksList.Enabled = true;
            if (tasksList.SelectedIndex >= 0)
            {
                mode = 2;
                tasksList.Enabled = false;
                editTextBox.Visible = true;
                editTextBox.Focus();
                editTextBox.Text = tasksList.SelectedItem.ToString();

            }
            else
            {
                editTextBox.Text = "";
                editTextBox.Visible = false; //przycisk edytuj może zostać naciśnięty w trakcie Dodawania, to w przypadku błędu ukryje pole teksowe
                tasksList.Enabled = true;
                MessageBox.Show("Trzeba zaznaczyć zadanie do edycji", "Błąd");
            }

        }

        private void DeleteAddButton_Click(object sender, EventArgs e)
        {
            if (tasksList.Enabled == true && tasksList.SelectedIndex >= 0)
            {
                editTextBox.Text = "";
                editTextBox.Visible = false;
                deleteTask();

            }
            else
            {
                editTextBox.Text = "";
                editTextBox.Visible = false;
                MessageBox.Show("Trzeba zaznaczyć zadanie do usunięcia", "Błąd");
                tasksList.Enabled = true;
            }
        }

        /*************************************************************************************************************************************************************************/




        /***********************************************************OBSLUGA KLAWISZY DLA TEXT BOXA********************************************************************************/
        private void EditTextBox_KeyDown(object sender, KeyEventArgs e)
        {
           //pole tekstowe pojawia się tylko wówczas gdy dodawane będzie nowe zadane, bądź edytowane już istniejące, rozpoznawane na podstawie trybu (mode) 
           if(e.KeyCode == Keys.Enter && editTextBox.Visible == true)
            {
                if(mode == 1)
                {
                    if (editTextBox.Text != "")
                    {
                        toDoList.Add(DataBaseSession.addTaskToDo(taskCalendar.SelectionRange.Start.ToShortDateString(), editTextBox.Text));
                        tasksList.Items.Add(editTextBox.Text);
                        editTextBox.Visible = false;
                        tasksList.Enabled = true;
                        tasksList.SelectedIndex = tasksList.Items.Count - 1;    //by zaznaczone było ostatnie zadanie(właśnie dodane), niezależnie od tego które zaznaczone było wcześniej
                        MessageBox.Show("Dodane!");
                        mode = 0;
                    }
                    else
                    {
                        MessageBox.Show("Pole tekstowe nie może być puste!", "Błąd");
                    }
                }
                else if(mode == 2)
                {
                    if (editTextBox.Text != "")
                    {
                        int x = tasksList.SelectedIndex;
                        DataBaseSession.editTaskToDo(toDoList[x].Id, editTextBox.Text);
                        MessageBox.Show("Edytowane");
                        tasksList.Items[tasksList.SelectedIndex] = editTextBox.Text; 
                        editTextBox.Visible = false;
                        tasksList.Enabled = true;
                        mode = 0;
                    }
                    else
                    {
                        MessageBox.Show("Pole tekstowe nie może być puste!", "Błąd");
                    }

                }

                tasksList.Focus();
                
            }

            if(e.KeyCode == Keys.Escape) //jeśli pole tekstowe jest widoczne - czyli w trybie edycji lub dodawania, esc przenosi do listy, wyłączając jednocześnie tryb dodawania/edycji
            {
                    editTextBox.Text = "";
                    editTextBox.Visible = false;
                    tasksList.Enabled = true;
                    tasksList.Focus();
                    mode = 0;                                   
            }
        }
        /*************************************************************************************************************************************************************************/



        /*********************************************************************OBSLUGA KLAWISZY DLA LISTY**************************************************************************/
        private void TasksList_KeyDown(object sender, KeyEventArgs e)
        {
           
            if(e.KeyCode == Keys.Escape)
            {
                taskCalendar.Focus();
                tasksList.SelectedIndex = -1;
            }

            //edycja w trakcie przeglądania zadań
            if (e.KeyCode == Keys.Enter)
            {
                editTextBox.Text = "";
                editTextBox.Visible = true;
                tasksList.Enabled = false;
                editTextBox.Focus();
                mode = 2;
            }
            
            //usunięcie w trakcie przegladania zadań
            if(e.KeyCode == Keys.Delete)
            {
                deleteTask();
            }

            //dodanie w trakcie przeglądania 
            if(e.KeyCode == Keys.Insert)
            {
                mode = 1;
                tasksList.Enabled = false;
                editTextBox.Visible = true;
                editTextBox.Focus();
                editTextBox.Text = "";
            }
        }

        /*************************************************************************************************************************************************************************/




        //Reakcja na zaznaczenie/odznaczenie pola checkbox - za pomocą funkcji changeCheckStatus zmieniana jest wartość stutusu w bazie dla konkretnego zadania
        private void TasksList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked) DataBaseSession.changeCheckStatus(toDoList[e.Index].Id, 1); //jeśli pole zostało zaznaczone

            else DataBaseSession.changeCheckStatus(toDoList[e.Index].Id, 0);  //jeśli zostało odznaczone
        }



        //Reakcja na przycisk Tab w trakcie przegladania kalendarza, przełączenie na listę
        private void TaskCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                tasksList.Focus();
            }
        }

        //Pomocnicza funkcja do usuwania, by uniknąć pisania dwa razy tego samego w kodzie
        private void deleteTask()
        {
            DialogResult dialogResult = MessageBox.Show("Czy na pewno chcesz usunąć to zadanie z listy?", "Uwaga", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                int x = tasksList.SelectedIndex;
                tasksList.Items.RemoveAt(x);
                DataBaseSession.deleteTaskToDo(toDoList[x].Id);
                toDoList.RemoveAt(x);

                if (x > 0)
                {
                    tasksList.SelectedIndex = x - 1; //aby zaznaczone zostało zadanie, które było przed usuniętym
                }
            }
            
        }

    }
}
