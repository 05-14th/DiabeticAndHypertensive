using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class MIStockIn : Form
    {
        string connectionString = "Server=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        public static DataGridViewRow selectedrow;
        DataTable dtDiabetic = new DataTable();
        private AutoCompleteStringCollection codeSuggestions;
        
        

        public MIStockIn()
        {
            InitializeComponent(); 
        }

        private void MIStockIn_Load(object sender, EventArgs e)
        {
            LoadAllStockIn();
            LoadCodeSuggestions();
           // Loadinventory();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string category = cbCategory.Text;
            DateTime date = DateTime.Parse(dtpRecieved.Text);
            string name = txtRname.Text;
            string code = txtCode.Text;
            string medname = txtMedName.Text;
            string brand = txtBrand.Text;
            string qty = txtQty.Text;
            DateTime exdate = DateTime.Parse(dtpExpiration.Text);
            
            string currrentStock = lblCurrentStock.Text;
            string recievestock = label5.Text;
            string receiving = textBox1.Text;


            if (string.IsNullOrWhiteSpace(cbCategory.Text) || string.IsNullOrWhiteSpace(dtpRecieved.Text) || string.IsNullOrWhiteSpace(txtRname.Text) || string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtMedName.Text) || string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txtQty.Text) || string.IsNullOrWhiteSpace(dtpExpiration.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectedCategory = cbCategory.SelectedItem.ToString();

                string insertquery = "";

                switch (selectedCategory)
                {
                    case "Diabetic Medicine":
                        insertquery = "INSERT INTO StockInDiabetic(category,date,name,code,medname,brand,qty,exdate)VALUES(@category,@date,@name,@code,@medname,@brand,@qty,@exdate)";
                        
                        break;

                    case "Hypertensive Medicine":
                        insertquery = "INSERT INTO StockInHypertensive(category,date,name,code,medname,brand,qty,exdate)VALUES(@category,@date,@name,@code,@medname,@brand,@qty,@exdate)";
                        break;
                }

                using (SqlCommand cmd = new SqlCommand(insertquery, connection))
                {
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@medname", medname);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@exdate", exdate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Medicine stocks added successfully!\n\nMedicine Name:{medname}\nMedicine Code:{code}\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }


                    else
                    {
                        MessageBox.Show("Failed to add medicine stocks. Press OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

               
            }
        }
        

        private void dgvStockIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCode.Text = dgvStockIn.CurrentRow.Cells[1].Value.ToString();
            txtMedName.Text = dgvStockIn.CurrentRow.Cells[2].Value.ToString();
            txtBrand.Text = dgvStockIn.CurrentRow.Cells[3].Value.ToString();
            cbCategory.Text = dgvStockIn.CurrentRow.Cells[4].Value.ToString();
            txtQty.Text = dgvStockIn.CurrentRow.Cells[5].Value.ToString();
            dtpExpiration.Text = dgvStockIn.CurrentRow.Cells[6].Value.ToString();
            txtRname.Text = dgvStockIn.CurrentRow.Cells[7].Value.ToString();
            dtpRecieved.Text = dgvStockIn.CurrentRow.Cells[8].Value.ToString();

            if (e.RowIndex >= 0)
            {
                selectedrow = dgvStockIn.Rows[e.RowIndex];

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string category = cbCategory.Text;
            DateTime date = DateTime.Parse(dtpRecieved.Text);
            string name = txtRname.Text;
            string code = txtCode.Text;
            string medname = txtMedName.Text;
            string brand = txtBrand.Text;
            string qty = txtQty.Text;
            DateTime exdate = DateTime.Parse(dtpExpiration.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectedCategory = cbCategory.SelectedItem.ToString();

                string updatequery = "";

                switch (selectedCategory)
                {
                    case "Diabetic Medicine":
                        updatequery = "Update StockInDiabetic Set category=@category,date=@date,name=@name,medname=@medname,brand=@brand,qty=@qty,exdate=@exdate WHERE code=@code";
                        break;

                    case "Hypertensive Medicine":
                        updatequery = "Update StockInHypertensive Set category=@category,date=@date,name=@name,medname=@medname,brand=@brand,qty=@qty,exdate=@exdate WHERE code=@code";
                        break;
                }

                using (SqlCommand cmd = new SqlCommand(updatequery, connection))
                {
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@medname", medname);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@exdate", exdate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Medicine stocks updated successfully..Press OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update medicine stocks.. Press OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Console.Out.WriteLine(code);
                    deleteRecord(code);
                    dgvStockIn.Rows.Remove(selectedrow);
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

                string deleteQueryDiabetic = "DELETE FROM StockInDiabetic WHERE code = @code";
                string deleteQueryHypertensive = "DELETE FROM StockInHypertensive WHERE code = @code";

                using (SqlCommand cmdDiabetic = new SqlCommand(deleteQueryDiabetic, connection))
                using (SqlCommand cmdHypertensive = new SqlCommand(deleteQueryHypertensive, connection))
                {
                    cmdDiabetic.Parameters.AddWithValue("@code", code);
                    cmdHypertensive.Parameters.AddWithValue("@code", code);

                    // Execute the DELETE statements
                    int rowsAffectedDiabetic = cmdDiabetic.ExecuteNonQuery();
                    int rowsAffectedHypertensive = cmdHypertensive.ExecuteNonQuery();

                    // Check if at least one row is affected in either table
                    if (rowsAffectedDiabetic > 0 || rowsAffectedHypertensive > 0)
                    {
                        MessageBox.Show("Record deleted successfully.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Record not found or not deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void LoadAllStockIn()
        {
            dgvStockIn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM StockInDiabetic", connection);
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter("SELECT * FROM StockInHypertensive", connection);


                adapterDiabetic.Fill(dtDiabetic);
                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Merge the data from both tables
                dtDiabetic.Merge(dtHypertensive);

                dgvStockIn.DataSource = dtDiabetic;

                dgvStockIn.Columns["Id"].HeaderText = "Number";
                dgvStockIn.Columns["category"].HeaderText = "Medicine Category";
                dgvStockIn.Columns["date"].HeaderText = "Date Recieved";
                dgvStockIn.Columns["date"].Visible = false;
                dgvStockIn.Columns["name"].HeaderText = "Reciever's Name";
                dgvStockIn.Columns["name"].Visible = false;
                dgvStockIn.Columns["code"].HeaderText = "Medicine Code";
                dgvStockIn.Columns["medname"].HeaderText = "Medicine Name";
                dgvStockIn.Columns["brand"].HeaderText = "Brand";
                dgvStockIn.Columns["qty"].HeaderText = "Quantity";
                dgvStockIn.Columns["exdate"].HeaderText = "Expiration Date";

                dgvStockIn.Columns["code"].DisplayIndex = 0;
                dgvStockIn.Columns["medname"].DisplayIndex = 1;
                dgvStockIn.Columns["brand"].DisplayIndex = 2;
                dgvStockIn.Columns["category"].DisplayIndex = 3;
                dgvStockIn.Columns["qty"].DisplayIndex = 4;
                dgvStockIn.Columns["exdate"].DisplayIndex = 5;
                dgvStockIn.Columns["name"].DisplayIndex = 6;
                dgvStockIn.Columns["date"].DisplayIndex = 7;
                dgvStockIn.Columns["Id"].Visible = false;
                DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockIn.ColumnHeadersDefaultCellStyle = headerStyle;
                dgvStockIn.RowTemplate.Height = 35;
                dgvStockIn.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockIn.Columns["code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockIn.Columns["qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockIn.Columns["exdate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvStockIn.Columns["brand"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
                dgvStockIn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvStockIn.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                // Set the Resizable property to False for each column you don't want to be resizable
                dgvStockIn.Columns["category"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["date"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["name"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["code"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["medname"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["brand"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["qty"].Resizable = DataGridViewTriState.False;
                dgvStockIn.Columns["exdate"].Resizable = DataGridViewTriState.False;
            }
        }

        private void UpdateInventoryDiabetic()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert into InventoryDiabetic from StockInDiabetic
                string updatequery = "Update InventoryDiabetic Set category=@category,medname=@medname,brand=@brand,qty=@qty WHERE code=@code";

                using (SqlCommand cmdIn = new SqlCommand(updatequery, connection))
                {
                    cmdIn.ExecuteNonQuery();
                } 
            }
        }

        private void UpdateInventoryHypertensive()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert into InventoryHypertensive from StockInHypertensive
                string updatequery = "Update InventoryHypertensive Set category=@category,medname=@medname,brand=@brand,qty=@qty WHERE code=@code";
                using (SqlCommand cmdIn = new SqlCommand(updatequery, connection))
                {

                    cmdIn.ExecuteNonQuery();
                }   
            }
        }

        private void txtRname_TextChanged(object sender, EventArgs e)
        {
            // Remove any digits from the entered text
            string filteredText = new string(txtRname.Text.Where(c => !char.IsDigit(c)).ToArray());

            // Ensure that the TextBox does not contain null or empty values
            if (!string.IsNullOrEmpty(filteredText))
            {
                // Use TextInfo.ToTitleCase to uppercase the first letter of each word
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtRname.Text = textInfo.ToTitleCase(filteredText);
            }
            else
            {
                // If the filtered text is empty, set the TextBox text to empty
                txtRname.Text = string.Empty;
            }

            // Set the cursor position at the end of the text
            txtRname.SelectionStart = txtRname.Text.Length;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            string enteredCode = txtCode.Text.Trim();

            if (!string.IsNullOrEmpty(enteredCode))
            {
                DataGridViewRow foundRow = null;

                foreach (DataGridViewRow row in dgvStockIn.Rows)
                {
                    if (!row.IsNewRow && row.Cells["code"].Value.ToString().Equals(enteredCode, StringComparison.OrdinalIgnoreCase))
                    {
                        foundRow = row;
                        break;
                    }
                }

                if (foundRow != null)
                {
                    // Get the corresponding "Medicine Name" value
                    string medName = foundRow.Cells["medname"].Value.ToString();
                    string qty = foundRow.Cells["qty"].Value.ToString();
                    string brand = foundRow.Cells["brand"].Value.ToString();

                    // Populate txtMedName
                    txtMedName.Text = medName;
                    lblCurrentStock.Text = qty;
                    txtBrand.Text = brand;
                    

                }
                else
                {
                    // Code not found, clear txtMedName or handle it accordingly
                    txtMedName.Text = string.Empty;
                    lblCurrentStock.Text = string.Empty;
                    txtBrand.Text = string.Empty;
                }
            }
            else
            {
                // If txtCode is empty, clear txtMedName
                txtMedName.Text = string.Empty;
                lblCurrentStock.Text = string.Empty;
                txtBrand.Text = string.Empty;
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
            if (txtQty.Text.Any(char.IsLetter))
            {
                txtQty.Clear();
            }
            else
            {
                string inputText = new string(txtQty.Text.Where(char.IsDigit).ToArray());
            }
        }

        private void dgvStockIn_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current cell is in the QuantityColumn
            if (e.ColumnIndex == dgvStockIn.Columns["qty"].Index && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int quantity))
                {
                    // Format the quantity with commas
                    e.Value = $"{quantity:n0}";
                    e.FormattingApplied = true; // Set this to true to indicate that the formatting is applied
                }
            }
        }
        private void FilterData(string[] columns, string searchKeyword, DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the WHERE clause dynamically based on the specified columns
                string whereClause = string.Join(" OR ", columns.Select(column => $"{column} LIKE @searchKeyword"));
                whereClause += " AND [date] BETWEEN @fromDate AND @toDate"; // date range

                // Modify the SQL query based on your table structure and columns
                SqlDataAdapter adapterDiabetic = new SqlDataAdapter($"SELECT * FROM StockInDiabetic WHERE {whereClause}", connection);
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter($"SELECT * FROM StockInHypertensive WHERE {whereClause}", connection);

                adapterDiabetic.SelectCommand.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                adapterDiabetic.SelectCommand.Parameters.AddWithValue("@fromDate", fromDate);
                adapterDiabetic.SelectCommand.Parameters.AddWithValue("@toDate", toDate);

                adapterHypertensive.SelectCommand.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                adapterHypertensive.SelectCommand.Parameters.AddWithValue("@fromDate", fromDate);
                adapterHypertensive.SelectCommand.Parameters.AddWithValue("@toDate", toDate);

                DataTable dtDiabetic = new DataTable();
                adapterDiabetic.Fill(dtDiabetic);

                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Merge the data from both tables
                dtDiabetic.Merge(dtHypertensive);

                dgvStockIn.DataSource = dtDiabetic;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text;
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;
            FilterData(new[] { "category", "name", "code", "medname", "brand", "qty" }, searchKeyword, fromDate, toDate);

        }

        private void LoadDateAdded_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;
            FilterData(new[] { "date" }, "", fromDate, toDate);
        }
        private void LoadCodeSuggestions()
        {
            codeSuggestions = new AutoCompleteStringCollection();

            foreach (DataGridViewRow row in dgvStockIn.Rows)
            {
                if (!row.IsNewRow)
                {
                    string code = row.Cells["code"].Value.ToString();
                    codeSuggestions.Add(code);
                }
            }

            txtCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCode.AutoCompleteCustomSource = codeSuggestions;
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

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadAllStockIn();
        }

        private void txtSearch_MouseEnter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }

            
        }

        private void txtSearch_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCode.Text = dgvStockIn.CurrentRow.Cells[1].Value.ToString();
            txtMedName.Text = dgvStockIn.CurrentRow.Cells[2].Value.ToString();
            txtBrand.Text = dgvStockIn.CurrentRow.Cells[3].Value.ToString();
            cbCategory.Text = dgvStockIn.CurrentRow.Cells[4].Value.ToString();
            txtQty.Text = dgvStockIn.CurrentRow.Cells[5].Value.ToString();
            dtpExpiration.Text = dgvStockIn.CurrentRow.Cells[6].Value.ToString();
            txtRname.Text = dgvStockIn.CurrentRow.Cells[7].Value.ToString();
            dtpRecieved.Text = dgvStockIn.CurrentRow.Cells[8].Value.ToString();

            if (e.RowIndex >= 0)
            {
                selectedrow = dgvStockIn.Rows[e.RowIndex];

            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvStockIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
