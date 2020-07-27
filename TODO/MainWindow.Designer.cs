namespace TODO
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.taskCalendar = new System.Windows.Forms.MonthCalendar();
            this.tasksList = new System.Windows.Forms.CheckedListBox();
            this.addTaskButton = new System.Windows.Forms.Button();
            this.editTaskButton = new System.Windows.Forms.Button();
            this.deleteAddButton = new System.Windows.Forms.Button();
            this.editTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // taskCalendar
            // 
            this.taskCalendar.Location = new System.Drawing.Point(49, 18);
            this.taskCalendar.Name = "taskCalendar";
            this.taskCalendar.TabIndex = 0;
            this.taskCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.TaskCalendar_DateChanged);
            this.taskCalendar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaskCalendar_KeyDown);
            // 
            // tasksList
            // 
            this.tasksList.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tasksList.FormattingEnabled = true;
            this.tasksList.Location = new System.Drawing.Point(400, 12);
            this.tasksList.Name = "tasksList";
            this.tasksList.Size = new System.Drawing.Size(670, 334);
            this.tasksList.TabIndex = 1;
            this.tasksList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TasksList_ItemCheck);
            this.tasksList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TasksList_KeyDown);
            // 
            // addTaskButton
            // 
            this.addTaskButton.Location = new System.Drawing.Point(100, 207);
            this.addTaskButton.Name = "addTaskButton";
            this.addTaskButton.Size = new System.Drawing.Size(171, 49);
            this.addTaskButton.TabIndex = 2;
            this.addTaskButton.Text = "Dodaj";
            this.addTaskButton.UseVisualStyleBackColor = true;
            this.addTaskButton.Click += new System.EventHandler(this.AddTaskButton_Click);
            // 
            // editTaskButton
            // 
            this.editTaskButton.Location = new System.Drawing.Point(100, 282);
            this.editTaskButton.Name = "editTaskButton";
            this.editTaskButton.Size = new System.Drawing.Size(171, 49);
            this.editTaskButton.TabIndex = 5;
            this.editTaskButton.Text = "Edytuj";
            this.editTaskButton.UseVisualStyleBackColor = true;
            this.editTaskButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // deleteAddButton
            // 
            this.deleteAddButton.Location = new System.Drawing.Point(100, 363);
            this.deleteAddButton.Name = "deleteAddButton";
            this.deleteAddButton.Size = new System.Drawing.Size(171, 49);
            this.deleteAddButton.TabIndex = 6;
            this.deleteAddButton.Text = "Usuń";
            this.deleteAddButton.UseVisualStyleBackColor = true;
            this.deleteAddButton.Click += new System.EventHandler(this.DeleteAddButton_Click);
            // 
            // editTextBox
            // 
            this.editTextBox.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.editTextBox.Location = new System.Drawing.Point(400, 392);
            this.editTextBox.Name = "editTextBox";
            this.editTextBox.Size = new System.Drawing.Size(670, 35);
            this.editTextBox.TabIndex = 4;
            this.editTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditTextBox_KeyDown);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 452);
            this.Controls.Add(this.deleteAddButton);
            this.Controls.Add(this.editTaskButton);
            this.Controls.Add(this.editTextBox);
            this.Controls.Add(this.addTaskButton);
            this.Controls.Add(this.tasksList);
            this.Controls.Add(this.taskCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TODO";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditTextBox_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar taskCalendar;
        private System.Windows.Forms.CheckedListBox tasksList;
        private System.Windows.Forms.Button addTaskButton;
        private System.Windows.Forms.Button editTaskButton;
        private System.Windows.Forms.Button deleteAddButton;
        private System.Windows.Forms.TextBox editTextBox;
    }
}

