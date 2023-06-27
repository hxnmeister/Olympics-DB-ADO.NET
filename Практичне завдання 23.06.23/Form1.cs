using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Практичне_завдання_23._06._23
{
    public partial class Form1 : Form
    {
        DataTable dataTable = null;
        SqlCommand command = null;
        readonly string fillDataGridView = @"select A.Name, A.Surname, A.Patronymic, A.Country as [Origin Country], A.BirthDate as [Birth Date], A.GoldMedal as [Gold Medals],
                           A.SilverMedal as [Silver Medals], A.BronzeMedal as [Bronze Medals], KS.Name as [Kind of Sport], KS.NumberOfParticipants as [Number of Patricipants],
                           OI.Season, OI.HostCountryName as [Host Country], OI.HostCityName as [Host City], OI.Year
                           from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                           where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId";
        readonly SqlConnection connection = null;
        readonly SqlDataAdapter adapter = null;

        public Form1()
        {
            InitializeComponent();

            dataTable = new DataTable();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectToOlympicsDB"].ConnectionString);
            adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(fillDataGridView, connection);

            adapter.Fill(dataTable);
            OlympicsDataGridView.DataSource = dataTable;
        }

        private DataTable ReaderQuery(string request)
        {
            int columns;
            DataTable tempDataTable = new DataTable();
            SqlDataReader reader = null;
            command = new SqlCommand(request, connection);

            try
            {
                reader = command.ExecuteReader();
                columns = reader.FieldCount;

                for (int i = 0; i < columns; i++)
                {
                    tempDataTable.Columns.Add(reader.GetName(i));
                }

                while (reader.Read())
                {
                    DataRow row = tempDataTable.NewRow();

                    for (int i = 0; i < columns; i++)
                    {
                        row[i] = reader[i].ToString();
                    }


                    tempDataTable.Rows.Add(row);
                    row.AcceptChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader?.Close();
                command.Dispose();
            }

            return tempDataTable;
        }
        private void SaveChanges(string tableName)
        {
            DataTable changesDataTable = dataTable.GetChanges();

            try
            {
                if (changesDataTable != null)
                {
                    foreach (DataRow changedRow in changesDataTable.Rows)
                    {
                        if (changedRow.RowState == DataRowState.Added)
                        {
                            if (tableName == "Athletes")
                            {
                                command = new SqlCommand(@"insert into Athletes(Id, Name, Surname, Patronymic, Country, BirthDate, GoldMedal, SilverMedal, BronzeMedal)
                                                   values(@id, @name, @surname, @patronymic, @country, @birthDate, @goldMedal, @silverMedal, @bronzeMedal)", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = changedRow["Surname"];
                                command.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = changedRow["Patronymic"] ?? DBNull.Value;
                                command.Parameters.Add("@country", SqlDbType.VarChar).Value = changedRow["Country"];
                                command.Parameters.Add("@birthDate", SqlDbType.Date).Value = changedRow["BirthDate"];
                                command.Parameters.Add("@goldMedal", SqlDbType.Int).Value = changedRow["GoldMedal"];
                                command.Parameters.Add("@silverMedal", SqlDbType.Int).Value = changedRow["SilverMedal"];
                                command.Parameters.Add("@bronzeMedal", SqlDbType.Int).Value = changedRow["BronzeMedal"];
                            }
                            else if (tableName == "KindOfSports")
                            {
                                command = new SqlCommand(@"insert into KindOfSports(Id, Name, NumberOfParticipants)
                                                   values(@id, @name, @numberOfParticipants)", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                                command.Parameters.Add("@numberOfParticipants", SqlDbType.VarChar).Value = changedRow["NumberOfParticipants"];
                            }
                            else if (tableName == "OlympicsInfo")
                            {
                                command = new SqlCommand(@"insert into OlympicsInfo(Id, Season, HostCountryName, HostCityName, Year)
                                                   values(@id, @season, @hostCountryName, @hosyCityName, @year)", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@season", SqlDbType.VarChar).Value = changedRow["Season"];
                                command.Parameters.Add("@hostCountryName", SqlDbType.VarChar).Value = changedRow["HostCountryName"];
                                command.Parameters.Add("@hosyCityName", SqlDbType.VarChar).Value = changedRow["HostCityName"];
                                command.Parameters.Add("@year", SqlDbType.Int).Value = changedRow["Year"];

                            }
                            else if (tableName == "KindOfSportsAndAthletes")
                            {
                                command = new SqlCommand(@"insert into KindOfSportsAndAthletes(Id, KindOfSportId, AthleteId, Result)
                                                   values(@id, @kindOfSportId, @athleteId, @result)", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@kindOfSportId", SqlDbType.Int).Value = changedRow["KindOfSportId"];
                                command.Parameters.Add("@athleteId", SqlDbType.Int).Value = changedRow["AthleteId"];
                                command.Parameters.Add("@result", SqlDbType.Float).Value = changedRow["Result"];

                            }
                            else if (tableName == "OlympicsOverallInfo")
                            {
                                command = new SqlCommand(@"insert into OlympicsOverallInfo(Id, OlympicsInfoId, KindOfSportAndAthleteId)
                                                   values(@id, @olympicsInfoId, @kindOfSportAndAthleteId)", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@olympicsInfoId", SqlDbType.Int).Value = changedRow["OlympicsInfoId"];
                                command.Parameters.Add("@kindOfSportAndAthleteId", SqlDbType.Int).Value = changedRow["KindOfSportAndAthleteId"];

                            }
                        }
                        else if (changedRow.RowState == DataRowState.Deleted)
                        {
                            command = new SqlCommand($"delete from {tableName} where Id = @id", connection);
                            command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(changedRow["Id", DataRowVersion.Original]);
                        }
                        else if (changedRow.RowState == DataRowState.Modified)
                        {
                            if (tableName == "Athletes")
                            {
                                command = new SqlCommand(@"update Athletes set Name = @name, Surname = @surname, Patronymic = @patronymic, 
                                                   Country = @country, BirthDate = @birthDate, GoldMedal = @goldMedal, 
                                                   SilverMedal = @silverMedal, BronzeMedal = @bronzeMedal 
                                                   where Id = @id", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = changedRow["Surname"];
                                command.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = changedRow["Patronymic"];
                                command.Parameters.Add("@country", SqlDbType.VarChar).Value = changedRow["Country"];
                                command.Parameters.Add("@birthDate", SqlDbType.Date).Value = changedRow["BirthDate"];
                                command.Parameters.Add("@goldMedal", SqlDbType.Int).Value = changedRow["GoldMedal"];
                                command.Parameters.Add("@silverMedal", SqlDbType.Int).Value = changedRow["SilverMedal"];
                                command.Parameters.Add("@bronzeMedal", SqlDbType.Int).Value = changedRow["BronzeMedal"];
                            }
                            else if (tableName == "KindOfSports")
                            {
                                command = new SqlCommand(@"update KindOfSports set Name = @name, NumberOfParticipants = @numberOfParticipants
                                                   where Id = @id", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                                command.Parameters.Add("@numberOfParticipants", SqlDbType.Int).Value = changedRow["NumberOfParticipants"];
                            }
                            else if (tableName == "OlympicsInfo")
                            {
                                command = new SqlCommand(@"update OlympicsInfo set Season = @season, HostCountryName = @hostCountryName, HostCityName = @hostCityName,
                                                   Year = @year
                                                   where Id = @id", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@season", SqlDbType.VarChar).Value = changedRow["Season"];
                                command.Parameters.Add("@hostCountryName", SqlDbType.VarChar).Value = changedRow["HostCountryName"];
                                command.Parameters.Add("@hostCityName", SqlDbType.VarChar).Value = changedRow["HostCityName"];
                                command.Parameters.Add("@year", SqlDbType.Int).Value = changedRow["Year"];
                            }
                            else if (tableName == "KindOfSportsAndAthletes")
                            {
                                command = new SqlCommand(@"update KindOfSportsAndAthletes set KindOfSportId = @kindOfSportId, AthleteId = @athleteId,
                                                   Result = @result
                                                   where Id = @id", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@kindOfSportId", SqlDbType.Int).Value = changedRow["KindOfSportId"];
                                command.Parameters.Add("@athleteId", SqlDbType.Int).Value = changedRow["AthleteId"];
                                command.Parameters.Add("@result", SqlDbType.Int).Value = changedRow["Result"];
                            }
                            else if (tableName == "OlympicsOverallInfo")
                            {
                                command = new SqlCommand(@"update OlympicsOverallInfo set OlympicsInfoId = @olympicsInfoId, KindOfSportAndAthleteId = @kindOfSportAndAthleteId
                                                   where Id = @id", connection);

                                command.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                                command.Parameters.Add("@olympicsInfoId", SqlDbType.Int).Value = changedRow["OlympicsInfoId"];
                                command.Parameters.Add("@kindOfSportAndAthleteId", SqlDbType.Int).Value = changedRow["KindOfSportAndAthleteId"];
                            }
                        }
                    }

                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ChangeButton_Click(object sender, EventArgs e)
        {
            switch (OptionsComboBox.SelectedItem.ToString())
            {
                case "Athletes":
                    SaveChanges("Athletes");
                    break;
                case "Kind of Sports":
                    SaveChanges("KindOfSports");
                    break;
                case "Olympics Info":
                    SaveChanges("OlympicsInfo");
                    break;
                case "Kind of Sports and Athletes":
                    SaveChanges("KindOfSportsAndAthletes");
                    break;
                case "Olympics Overall Information":
                    SaveChanges("OlympicsOverallInfo");
                    break;
            }
        }

        private void EditorModeButton_Click(object sender, EventArgs e)
        {
            connection.Open();

            ChangeButton.Visible = true;
            OptionsComboBox.Visible = true;
            UserDataTextBox.Enabled = false;
            UserData1TextBox.Enabled = false;
            RequestsGroupBox.Enabled = false;
            EditorModeButton.Enabled = false;
            ExecuteRequestButton.Enabled = false;
            EditorModeExitButton.Enabled = true;
            OlympicsDataGridView.ReadOnly = false;
            UserData1TextBox.Text = string.Empty;
            UserDataTextBox.Text = string.Empty;
            OptionsComboBox.Text = "Select an option to change!";
        }

        private void OptionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeButton.Enabled = true;
            dataTable = new DataTable();

            switch (OptionsComboBox.SelectedItem.ToString())
            {
                case "Athletes":
                    dataTable = ReaderQuery(@"select * from Athletes");
                    break;
                case "Kind of Sports":
                    dataTable = ReaderQuery(@"select * from KindOfSports");
                    break;
                case "Olympics Info":
                    dataTable = ReaderQuery(@"select * from OlympicsInfo");
                    break;
                case "Kind of Sports and Athletes":
                    dataTable = ReaderQuery(@"select * from KindOfSportsAndAthletes");
                    break;
                case "Olympics Overall Information":
                    dataTable = ReaderQuery(@"select * from OlympicsOverallInfo");
                    break;
            }

            OlympicsDataGridView.DataSource = dataTable;
        }

        private void EditorModeExitButton_Click(object sender, EventArgs e)
        {
            ChangeButton.Visible = false;
            ChangeButton.Enabled = false;
            OptionsComboBox.Visible = false;
            EditorModeButton.Enabled = true;
            RequestsGroupBox.Enabled = true;
            RequestsComboBox.Text = "Choose Request!";
            EditorModeExitButton.Enabled = false;
            OlympicsDataGridView.ReadOnly = true;

            OlympicsDataGridView.DataSource = ReaderQuery(fillDataGridView);

            connection?.Close();
        }

        private void RequestsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = RequestsComboBox.SelectedIndex;

            if (index > 3 && index < 7) UserDataTextBox.Text = "Enter Olympics ID";
            else if (index == 7) UserDataTextBox.Text = "Enter the name of the sport";
            else if (index > 7) UserDataTextBox.Text = "Enter country";

            ExecuteRequestButton.Enabled = true;
            if (index == 10)
            {
                UserData1TextBox.Enabled = true;
                UserData1TextBox.Text = "Enter Olympics ID";
            }
            if (index > 3) UserDataTextBox.Enabled = true;
        }

        private void ExecuteRequestButton_Click(object sender, EventArgs e)
        {
            int index = RequestsComboBox.SelectedIndex;
            dataTable = new DataTable();

            try
            {
                if (index > 3 && (UserDataTextBox.Text == string.Empty && UserData1TextBox.Text == string.Empty))
                {
                    OlympicsDataGridView.DataSource = null;
                    throw new Exception();
                }

                switch (index)
                {
                    case 0:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 group by  A.Country
                                                                 order by sum(A.GoldMedal) desc", connection);
                        break;
                    case 1:
                        adapter.SelectCommand = new SqlCommand(@"select A.Name + ' ' + A.Surname as [Full Name], KS.Name as [Kind of Sport], sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 group by A.Name + ' ' + A.Surname, KS.Name
                                                                 order by KS.Name", connection);
                        break;
                    case 2:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) A.Country, max(A.GoldMedal) as [Most Gold Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 group by A.Country
                                                                 order by max(A.GoldMedal) desc", connection);
                        break;
                    case 3:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) HostCountryName as [Host Country], count(HostCountryName) as Amount
                                                                 from OlympicsInfo 
                                                                 group by HostCountryName
                                                                 order by count(HostCountryName) desc", connection);
                        break;
                    case 4:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and OI.Id = @id
                                                                 group by  A.Country
                                                                 order by sum(A.GoldMedal) desc", connection);
                        break;
                    case 5:
                        adapter.SelectCommand = new SqlCommand(@"select A.Name + ' ' + A.Surname as [Full Name], KS.Name as [Kind of Sport], sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and OI.Id = @id
                                                                 group by A.Name + ' ' + A.Surname, KS.Name
                                                                 order by KS.Name", connection);
                        break;
                    case 6:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) A.Country, max(A.GoldMedal) as [Most Gold Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and OI.Id = @id
                                                                 group by A.Country
                                                                 order by max(A.GoldMedal) desc", connection);
                        break;
                    case 7:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) A.Name + ' ' + A.Surname as [Full Name], sum(A.GoldMedal) as [Most Gold Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and KS.Name = @kind
                                                                 group by A.Name + ' ' + A.Surname
                                                                 order by sum(A.GoldMedal) desc", connection);

                        adapter.SelectCommand.Parameters.Add("@kind",SqlDbType.VarChar).Value = UserDataTextBox.Text.ToString();
                        break;
                    case 8:
                        adapter.SelectCommand = new SqlCommand(@"select Name + ' ' + Surname as [Full Name]
                                                                 from Athletes
                                                                 where Country = @country", connection);

                        break;
                    case 9:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals], sum(KSA.Result) as Result
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and A.Country = @country
                                                                 group by A.Country
                                                                 order by sum(KSA.Result) desc", connection);
                        break;
                    case 10:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals], sum(KSA.Result) as Result
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and A.Country = @country and OI.Id = @id
                                                                 group by A.Country
                                                                 order by sum(KSA.Result) desc", connection);

                        break;
                }

                if((index > 3 && index < 7) || index == 10) adapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = Int32.TryParse( index == 10 ? UserData1TextBox.Text.ToString() : UserDataTextBox.Text.ToString(), out int temp) ? temp : throw new Exception();
                if(index > 7) adapter.SelectCommand.Parameters.Add("@country", SqlDbType.VarChar).Value = UserDataTextBox.Text.ToString();

                adapter.Fill(dataTable);
                OlympicsDataGridView.DataSource = dataTable;

                adapter.SelectCommand.Parameters.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Check the correctness of the data entered or contact the administrator!");
            }

            RequestsComboBox.Text = "Choose Request!";
            UserDataTextBox.Enabled = false;
            UserDataTextBox.Text = string.Empty;
            UserData1TextBox.Text = string.Empty;
            UserData1TextBox.Enabled = false;
            ExecuteRequestButton.Enabled = false;
        }

        private void UserDataTextBox_Enter(object sender, EventArgs e)
        {
            UserDataTextBox.Clear();
        }

        private void UserData1TextBox_Enter(object sender, EventArgs e)
        {
            UserData1TextBox.Clear();
        }
    }
}
