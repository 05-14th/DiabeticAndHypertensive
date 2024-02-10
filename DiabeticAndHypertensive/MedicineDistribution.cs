using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace DiabeticAndHypertensive
{
    public partial class MedicineDistribution : Form
    {
        string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True;";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        public static DataGridViewRow selectedrow;
        DataTable dtDiabetic = new DataTable();
        private AutoCompleteStringCollection codeSuggestions;
        public MedicineDistribution()
        {
            InitializeComponent();
        }

        private void MedicineDistribution_Load(object sender, EventArgs e)
        {
            LoadAllStockOut();
            LoadCodeSuggestions();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string category = cbCategory.Text;
            DateTime ddate = DateTime.Parse(dtpDispense.Text);
            string dname = txtbyname.Text;
            string code = txtCode.Text;
            string medname = txtMedName.Text;
            string brand = txtBrand.Text;
            string rname = txttoname.Text;
            string phealth = txtphealth.Text;
            string brgy = cbBarangay.Text;
            string qty = txtQty.Text;
            DateTime exdate = DateTime.Parse(dtpExpiration.Text);

            if (string.IsNullOrWhiteSpace(cbCategory.Text) || string.IsNullOrWhiteSpace(dtpDispense.Text) || string.IsNullOrWhiteSpace(txtbyname.Text) || string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtMedName.Text) || string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txttoname.Text) || string.IsNullOrWhiteSpace(txtphealth.Text) ||  string.IsNullOrWhiteSpace(cbBarangay.Text) || string.IsNullOrWhiteSpace(txtQty.Text) || string.IsNullOrWhiteSpace(dtpExpiration.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectedCategory = cbCategory.SelectedItem.ToString();

                string insertquery = "INSERT INTO StockOutDiabetic(category,ddate,dname,code,medname,brand,rname,phealth,brgy,qty,exdate)VALUES(@category,@ddate,@dname,@code,@medname,@brand,@rname,@phealth,@brgy,@qty,@exdate)";
                        
                using (SqlCommand cmd = new SqlCommand(insertquery, connection))
                {
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@ddate", ddate);
                    cmd.Parameters.AddWithValue("@dname", dname);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@medname", medname);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@rname", rname);
                    cmd.Parameters.AddWithValue("@phealth", phealth);
                    cmd.Parameters.AddWithValue("@brgy", brgy);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@exdate", exdate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Medicine information added successfully!\n\nMedicine Name:{medname}\nMedicine Code:{code}\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateTable();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add medicine information. Press OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvStockOut_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cbCategory.Text = dgvStockOut.CurrentRow.Cells[1].Value.ToString();
            dtpDispense.Text = dgvStockOut.CurrentRow.Cells[2].Value.ToString();
            txtbyname.Text = dgvStockOut.CurrentRow.Cells[3].Value.ToString();
            txtCode.Text = dgvStockOut.CurrentRow.Cells[4].Value.ToString();
            txtMedName.Text = dgvStockOut.CurrentRow.Cells[5].Value.ToString();
            txtBrand.Text = dgvStockOut.CurrentRow.Cells[6].Value.ToString();
            txttoname.Text = dgvStockOut.CurrentRow.Cells[7].Value.ToString();
            txtphealth.Text = dgvStockOut.CurrentRow.Cells[8].Value.ToString();
            cbBarangay.Text = dgvStockOut.CurrentRow.Cells[9].Value.ToString();
            txtQty.Text = dgvStockOut.CurrentRow.Cells[10].Value.ToString();
            dtpExpiration.Text = dgvStockOut.CurrentRow.Cells[11].Value.ToString();

            if (e.RowIndex >= 0)
            {
                selectedrow = dgvStockOut.Rows[e.RowIndex];

            }
        }

        private void updateTable()
        {
            dgvStockOut.Refresh();
            dgvStockOut.Update();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string category = cbCategory.Text;
            DateTime ddate = DateTime.Parse(dtpDispense.Text);
            string dname = txtbyname.Text;
            string code = txtCode.Text;
            string medname = txtMedName.Text;
            string brand = txtBrand.Text;
            string rname = txttoname.Text;
            string phealth = txtphealth.Text;
            string brgy = cbBarangay.Text;
            string qty = txtQty.Text;
            DateTime exdate = DateTime.Parse(dtpExpiration.Text);

            if (string.IsNullOrWhiteSpace(cbCategory.Text) || string.IsNullOrWhiteSpace(dtpDispense.Text) || string.IsNullOrWhiteSpace(txtbyname.Text) || string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtMedName.Text) || string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txttoname.Text) || string.IsNullOrWhiteSpace(txtphealth.Text)|| string.IsNullOrWhiteSpace(cbBarangay.Text) || string.IsNullOrWhiteSpace(txtQty.Text) || string.IsNullOrWhiteSpace(dtpExpiration.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectedCategory = cbCategory.SelectedItem.ToString();

                string updatequery = "";

                updatequery = "Update StockOutDiabetic Set category=@category,ddate=@ddate,dname=@dname,medname=@medname,brand=@brand,rname=@rname,phealth=@phealth,brgy=@brgy,qty=@qty,exdate=@exdate WHERE code=@code";
                    
                using (SqlCommand cmd = new SqlCommand(updatequery, connection))
                {
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@ddate", ddate);
                    cmd.Parameters.AddWithValue("@dname", dname);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@medname", medname);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@rname", rname);
                    cmd.Parameters.AddWithValue("@phealth", phealth);
                    cmd.Parameters.AddWithValue("@brgy", brgy);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@exdate", exdate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Medicine information added successfully!\n\nMedicine Name:{medname}\nMedicine Code:{code}\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateTable();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add medicine information. Press OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedrow != null)
            {
                DialogResult result = MessageBox.Show("Do you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string code = selectedrow.Cells["code"].Value.ToString();
                    deleteRecord(code);
                    dgvStockOut.Rows.Remove(selectedrow);
                    MessageBox.Show("Record deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    selectedrow = null; // Reset selectedrow after deletion
                    updateTable();
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("Record not deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Select a record to delete.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void deleteRecord(string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
    

                string deleteQuery = $"DELETE FROM StockOutDiabetic WHERE code = @code";
                       
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@code", code);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected <= 0)
                    {
                        MessageBox.Show("Record not found or not deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void LoadAllStockOut()
        {
            dgvStockOut.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM StockOutDiabetic", connection);

                adapterDiabetic.Fill(dtDiabetic);
   
               
                dgvStockOut.DataSource = dtDiabetic;

                dgvStockOut.Columns["Id"].HeaderText = "Number";
                dgvStockOut.Columns["category"].HeaderText = "Medicine Type";
                dgvStockOut.Columns["ddate"].HeaderText = "Dispensed Date";
                dgvStockOut.Columns["dname"].HeaderText = "Dispensed By";
                dgvStockOut.Columns["code"].HeaderText = "Medicine Code";
                dgvStockOut.Columns["medname"].HeaderText = "Medicine Name";
                dgvStockOut.Columns["brand"].HeaderText = "Brand";
                dgvStockOut.Columns["rname"].HeaderText = "Dispensed To";
                dgvStockOut.Columns["phealth"].HeaderText = "PhilHealth";
                dgvStockOut.Columns["brgy"].HeaderText = "Barangay";
                dgvStockOut.Columns["qty"].HeaderText = "Quantity";
                dgvStockOut.Columns["exdate"].HeaderText = "Expiration Date";

                dgvStockOut.Columns["Id"].DisplayIndex = 0;
                dgvStockOut.Columns["category"].DisplayIndex = 6;
                dgvStockOut.Columns["ddate"].DisplayIndex = 7;
                dgvStockOut.Columns["dname"].DisplayIndex = 8;
                dgvStockOut.Columns["code"].DisplayIndex = 1;
                dgvStockOut.Columns["medname"].DisplayIndex = 2;
                dgvStockOut.Columns["brand"].DisplayIndex = 3;
                dgvStockOut.Columns["rname"].DisplayIndex = 9;
                dgvStockOut.Columns["phealth"].DisplayIndex = 10;
                dgvStockOut.Columns["brgy"].DisplayIndex = 11;
                dgvStockOut.Columns["qty"].DisplayIndex = 4;
                dgvStockOut.Columns["exdate"].DisplayIndex = 5;
                dgvStockOut.Columns["Id"].Visible = false;
                dgvStockOut.Columns["category"].Visible = false;
                dgvStockOut.Columns["dname"].Visible = false;
                dgvStockOut.Columns["brand"].Visible = false;
                dgvStockOut.Columns["exdate"].Visible = false;
                DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockOut.ColumnHeadersDefaultCellStyle = headerStyle;
                dgvStockOut.RowTemplate.Height = 35;
                dgvStockOut.Columns["ddate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockOut.Columns["code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockOut.Columns["qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockOut.Columns["exdate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockOut.Columns["brand"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
                dgvStockOut.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvStockOut.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                // Set the Resizable property to False for each column you don't want to be resizable
                dgvStockOut.Columns["category"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["ddate"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["dname"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["code"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["medname"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["brand"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["qty"].Resizable = DataGridViewTriState.False;
                dgvStockOut.Columns["exdate"].Resizable = DataGridViewTriState.False;
            }
        }

        private void InsertIntoInventoryHypertensive()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert into InventoryHypertensive from StockOutHypertensive
                string queryOut = @"INSERT INTO InventoryHypertensive(category, code, medname, brand, [out])
                            SELECT category, code, medname, brand, qty
                            FROM StockOutHypertensive";

                using (SqlCommand cmdOut = new SqlCommand(queryOut, connection))
                {

                    cmdOut.ExecuteNonQuery();
                }
            }
        }
        private void LoadCodeSuggestions()
        {
            codeSuggestions = new AutoCompleteStringCollection();

            // Load codes from InventoryHypertensive
            LoadCodesFromTable("InventoryHypertensive", "code");

            // Load codes from InventoryDiabetic
            LoadCodesFromTable("InventoryDiabetic", "code");

            txtCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCode.AutoCompleteCustomSource = codeSuggestions;
        }
        private void LoadCodesFromTable(string tableName, string columnName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT DISTINCT {columnName} FROM {tableName}";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string code = reader[columnName].ToString();
                            codeSuggestions.Add(code);
                        }
                    }
                }
            }
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            string enteredCode = txtCode.Text.Trim();

            if (!string.IsNullOrEmpty(enteredCode))
            {
                Tuple<string, string> medInfoHypertensive = GetMedicineInfoFromTable("StockInDiabetic", enteredCode);
                if (medInfoHypertensive != null)
                {
                    txtMedName.Text = medInfoHypertensive.Item1; // Medicine Name
                    txtBrand.Text = medInfoHypertensive.Item2;   // Brand
                    return; // Exit the method if found in InventoryHypertensive
                }

                // Code not found in either table, clear txtMedName and txtBrand or handle it accordingly
                txtMedName.Text = string.Empty;
                txtBrand.Text = string.Empty;
            }
            else
            {
                // If txtCode is empty, clear txtMedName and txtBrand
                txtMedName.Text = string.Empty;
                txtBrand.Text = string.Empty;
            }
        }

        private Tuple<string, string> GetMedicineInfoFromTable(string tableName, string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT TOP 1 medname, brand FROM {tableName} WHERE code = @code";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@code", code);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string medname = reader["medname"].ToString();
                            string brand = reader["brand"].ToString();
                            return Tuple.Create(medname, brand);
                        }
                    }
                }

                return null; // Return null if not found
            }
        }
        


        private void txtBrand_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtBrand.Text = textInfo.ToTitleCase(txtBrand.Text);

                txtBrand.SelectionStart = txtBrand.Text.Length;
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                // Remove any commas from the input
                string cleanedText = txtQty.Text.Replace(",", "");

                // Parse the quantity as a number
                if (int.TryParse(cleanedText, out int quantity))
                {
                    // Format the quantity with commas
                    string formattedQuantity = $"{quantity:n0}";

                    // Set the formatted text to the TextBox
                    txtQty.Text = formattedQuantity;

                    // Move the cursor to the end of the text
                    txtQty.SelectionStart = txtQty.Text.Length;
                }
                else
                {
                    // If the entered text is not a valid numeric input, revert to the previous text
                    txtQty.Text = txtQty.Text.Substring(0, txtQty.Text.Length - 1);
                    txtQty.SelectionStart = txtQty.Text.Length;
                }
            }
        }

        private void txtbyname_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtbyname.Text))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtbyname.Text = textInfo.ToTitleCase(txtbyname.Text);

                txtbyname.SelectionStart = txtbyname.Text.Length;
            }
        }

        private void txttoname_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txttoname.Text))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txttoname.Text = textInfo.ToTitleCase(txttoname.Text);

                txttoname.SelectionStart = txttoname.Text.Length;
            }
        }

        private void txtphealth_TextChanged(object sender, EventArgs e)
        {
            if (txtphealth.Text.Any(char.IsLetter))
            {
                txtphealth.Clear();
            }
            else
            {
                string inputText = new string(txtphealth.Text.Where(char.IsDigit).ToArray());

                // Check if the input length is within the expected range
                if (inputText.Length >= 12)
                {
                    // Format the numeric string as desired
                    string formattedText = string.Format("{0} {1} {2}", inputText.Substring(0, 4), inputText.Substring(4, 4), inputText.Substring(8));

                    // Update the text box with the formatted text
                    txtphealth.Text = formattedText;

                    // Set the cursor position to the end of the text box
                    txtphealth.SelectionStart = txtphealth.Text.Length;
                }
            }
        }

        private void FilterData(string[] columns, string searchKeyword, DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the WHERE clause dynamically based on the specified columns
                string whereClause = string.Join(" OR ", columns.Select(column => $"{column} LIKE '{searchKeyword}'"));
                whereClause += " AND [ddate] BETWEEN @fromDate AND @toDate"; // date range

                // Modify the SQL query based on your table structure and columns
                string query = $"SELECT * FROM StockOutDiabetic WHERE {whereClause}";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvStockOut.DataSource = dt;
                }
                connection.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text;
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;
            FilterData(new[] { "category", "dname", "code", "medname", "brand", "rname", "phealth", "brgy", "qty" }, searchKeyword, fromDate, toDate);
        }

        private void LoadDateAdded_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;
            FilterData(new[] { "ddate" }, "", fromDate, toDate);
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            phSearch.Visible = false;
            if (textBox.Text == phSearch.Text)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = SystemColors.WindowText; // Restore the original text color
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.Black;
            }
        }
    }
}
