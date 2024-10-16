using System;
using System.Windows.Forms;

namespace TodoListApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetupDataGridViews();
        }

        // Method to setup the DataGridViews
        private void SetupDataGridViews()
        {
            // Setup for Active Tasks DataGridView
            dgvActiveTasks.Columns.Add("Task", "Task");
            dgvActiveTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActiveTasks.CellDoubleClick += dgvActiveTasks_CellDoubleClick;

            // Setup for Done Tasks DataGridView
            dgvDoneTasks.Columns.Add("Task", "Task");
            dgvDoneTasks.Columns.Add("CompletedAt", "Completed At");
            dgvDoneTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoneTasks.ReadOnly = true;
        }

        // Add Task button click handler
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewTask.Text))
            {
                dgvActiveTasks.Rows.Add(txtNewTask.Text);
                txtNewTask.Clear();  // Clear the input field after adding the task
            }
            else
            {
                MessageBox.Show("Please enter a task.");
            }
        }

        // Handle double click to mark task as Done
        private void dgvActiveTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Ensure double-clicking on a valid row
            {
                string task = dgvActiveTasks.Rows[e.RowIndex].Cells[0].Value.ToString();

                var result = MessageBox.Show($"Do you want to mark this task as Done?", "Mark as Done",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Move the task to Done list
                    dgvDoneTasks.Rows.Add(task, DateTime.Now.ToString("g"));
                    // Remove the task from the Active list
                    dgvActiveTasks.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}
