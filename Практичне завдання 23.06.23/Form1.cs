using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Практичне_завдання_23._06._23
{
    public partial class Form1 : Form
    {
        string fillDGV = @"select A.Name, A.Surname, A.Patronymic, A.Country as [Origin Country], A.BirthDate as [Birth Date], A.GoldMedal as [Gold Medals],
                           A.SilverMedal as [Silver Medals], A.BronzeMedal as [Bronze Medals], KS.Name as [Kind of Sport], KS.NumberOfParticipants as [Number of Patricipants],
                           OI.Season, OI.HostCountryName as [Host Country], OI.HostCityName as [Host City], OI.Year
                           from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                           where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId";
        DataTable dt = null;
        SqlCommand cmd = null;
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;

        public Form1()
        {
            InitializeComponent();

            dt = new DataTable();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectToOlympicsDB"].ConnectionString);
            adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(fillDGV, conn);

            adapter.Fill(dt);
            OlympicsDataGridView.DataSource = dt;
        }

        private void ReaderQuery(string request)
        {
            int columns;
            SqlDataReader reader = null;
            cmd = new SqlCommand(request, conn);
            dt = new DataTable();

            reader = cmd.ExecuteReader();
            columns = reader.FieldCount;

            for (int i = 0; i < columns; i++)
            {
                dt.Columns.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                DataRow row = dt.NewRow();

                for (int i = 0; i < columns; i++)
                {
                    row[i] = reader[i].ToString();
                }


                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            reader?.Close();
            cmd.Dispose();
        }
        private void SaveChanges(string tableName)
        {
            DataTable changesDataTable = dt.GetChanges();

            if (changesDataTable != null)
            {
                foreach (DataRow changedRow in changesDataTable.Rows)
                {
                    if (changedRow.RowState == DataRowState.Added)
                    {
                        if (tableName == "Athletes")
                        {
                            cmd = new SqlCommand(@"insert into Athletes(Id, Name, Surname, Patronymic, Country, BirthDate, GoldMedal, SilverMedal, BronzeMedal)
                                                   values(@id, @name, @surname, @patronymic, @country, @birthDate, @goldMedal, @silverMedal, @bronzeMedal)", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                            cmd.Parameters.Add("@surname", SqlDbType.VarChar).Value = changedRow["Surname"];
                            cmd.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = changedRow["Patronymic"] ?? DBNull.Value;
                            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = changedRow["Country"];
                            cmd.Parameters.Add("@birthDate", SqlDbType.Date).Value = changedRow["BirthDate"];
                            cmd.Parameters.Add("@goldMedal", SqlDbType.Int).Value = changedRow["GoldMedal"];
                            cmd.Parameters.Add("@silverMedal", SqlDbType.Int).Value = changedRow["SilverMedal"];
                            cmd.Parameters.Add("@bronzeMedal", SqlDbType.Int).Value = changedRow["BronzeMedal"];
                        }
                        else if (tableName == "KindOfSports")
                        {
                            cmd = new SqlCommand(@"insert into KindOfSports(Id, Name, NumberOfParticipants)
                                                   values(@id, @name, @numberOfParticipants)", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                            cmd.Parameters.Add("@numberOfParticipants", SqlDbType.VarChar).Value = changedRow["NumberOfParticipants"];
                        }
                        else if (tableName == "OlympicsInfo")
                        {
                            cmd = new SqlCommand(@"insert into OlympicsInfo(Id, Season, HostCountryName, HostCityName, Year)
                                                   values(@id, @season, @hostCountryName, @hosyCityName, @year)", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@season", SqlDbType.VarChar).Value = changedRow["Season"];
                            cmd.Parameters.Add("@hostCountryName", SqlDbType.VarChar).Value = changedRow["HostCountryName"];
                            cmd.Parameters.Add("@hosyCityName", SqlDbType.VarChar).Value = changedRow["HostCityName"];
                            cmd.Parameters.Add("@year", SqlDbType.Int).Value = changedRow["Year"];

                        }
                        else if (tableName == "KindOfSportsAndAthletes")
                        {
                            cmd = new SqlCommand(@"insert into KindOfSportsAndAthletes(Id, KindOfSportId, AthleteId, Result)
                                                   values(@id, @kindOfSportId, @athleteId, @result)", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@kindOfSportId", SqlDbType.Int).Value = changedRow["KindOfSportId"];
                            cmd.Parameters.Add("@athleteId", SqlDbType.Int).Value = changedRow["AthleteId"];
                            cmd.Parameters.Add("@result", SqlDbType.Float).Value = changedRow["Result"];

                        }
                        else if (tableName == "OlympicsOverallInfo")
                        {
                            cmd = new SqlCommand(@"insert into OlympicsOverallInfo(Id, OlympicsInfoId, KindOfSportAndAthleteId)
                                                   values(@id, @olympicsInfoId, @kindOfSportAndAthleteId)", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@olympicsInfoId", SqlDbType.Int).Value = changedRow["OlympicsInfoId"];
                            cmd.Parameters.Add("@kindOfSportAndAthleteId", SqlDbType.Int).Value = changedRow["KindOfSportAndAthleteId"];

                        }
                    }
                    else if (changedRow.RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand($"delete from {tableName} where Id = @id", conn);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(changedRow["Id", DataRowVersion.Original]);
                    }
                    else if (changedRow.RowState == DataRowState.Modified)
                    {
                        if (tableName == "Athletes")
                        {
                            cmd = new SqlCommand(@"update Athletes set Name = @name, Surname = @surname, Patronymic = @patronymic, 
                                                   Country = @country, BirthDate = @birthDate, GoldMedal = @goldMedal, 
                                                   SilverMedal = @silverMedal, BronzeMedal = @bronzeMedal 
                                                   where Id = @id", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                            cmd.Parameters.Add("@surname", SqlDbType.VarChar).Value = changedRow["Surname"];
                            cmd.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = changedRow["Patronymic"];
                            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = changedRow["Country"];
                            cmd.Parameters.Add("@birthDate", SqlDbType.Date).Value = changedRow["BirthDate"];
                            cmd.Parameters.Add("@goldMedal", SqlDbType.Int).Value = changedRow["GoldMedal"];
                            cmd.Parameters.Add("@silverMedal", SqlDbType.Int).Value = changedRow["SilverMedal"];
                            cmd.Parameters.Add("@bronzeMedal", SqlDbType.Int).Value = changedRow["BronzeMedal"];
                        }
                        else if (tableName == "KindOfSports")
                        {
                            cmd = new SqlCommand(@"update KindOfSports set Name = @name, NumberOfParticipants = @numberOfParticipants
                                                   where Id = @id", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = changedRow["Name"];
                            cmd.Parameters.Add("@numberOfParticipants", SqlDbType.Int).Value = changedRow["NumberOfParticipants"];
                        }
                        else if (tableName == "OlympicsInfo")
                        {
                            cmd = new SqlCommand(@"update OlympicsInfo set Season = @season, HostCountryName = @hostCountryName, HostCityName = @hostCityName,
                                                   Year = @year
                                                   where Id = @id", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@season", SqlDbType.VarChar).Value = changedRow["Season"];
                            cmd.Parameters.Add("@hostCountryName", SqlDbType.VarChar).Value = changedRow["HostCountryName"];
                            cmd.Parameters.Add("@hostCityName", SqlDbType.VarChar).Value = changedRow["HostCityName"];
                            cmd.Parameters.Add("@year", SqlDbType.Int).Value = changedRow["Year"];
                        }
                        else if (tableName == "KindOfSportsAndAthletes")
                        {
                            cmd = new SqlCommand(@"update KindOfSportsAndAthletes set KindOfSportId = @kindOfSportId, AthleteId = @athleteId,
                                                   Result = @result
                                                   where Id = @id", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@kindOfSportId", SqlDbType.Int).Value = changedRow["KindOfSportId"];
                            cmd.Parameters.Add("@athleteId", SqlDbType.Int).Value = changedRow["AthleteId"];
                            cmd.Parameters.Add("@result", SqlDbType.Int).Value = changedRow["Result"];
                        }
                        else if (tableName == "OlympicsOverallInfo")
                        {
                            cmd = new SqlCommand(@"update OlympicsOverallInfo set OlympicsInfoId = @olympicsInfoId, KindOfSportAndAthleteId = @kindOfSportAndAthleteId
                                                   where Id = @id", conn);

                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@olympicsInfoId", SqlDbType.Int).Value = changedRow["OlympicsInfoId"];
                            cmd.Parameters.Add("@kindOfSportAndAthleteId", SqlDbType.Int).Value = changedRow["KindOfSportAndAthleteId"];
                        }
                    }
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }


        private void ChangeButton_Click(object sender, EventArgs e)
        {
            switch (OptionsComboBox.SelectedIndex)
            {
                case 0:
                    SaveChanges("Athletes");
                    break;
                case 1:
                    SaveChanges("KindOfSports");
                    break;
                case 2:
                    SaveChanges("OlympicsInfo");
                    break;
                case 3:
                    SaveChanges("KindOfSportsAndAthletes");
                    break;
                case 4:
                    SaveChanges("OlympicsOverallInfo");
                    break;
            }

        }

        private void EditorModeButton_Click(object sender, EventArgs e)
        {
            conn.Open();

            ChangeButton.Visible = true;
            OptionsComboBox.Visible = true;
            OptionsComboBox.Text = "Select an option to change!";
            EditorModeButton.Enabled = false;
            EditorModeExitButton.Enabled = true;
            OlympicsDataGridView.ReadOnly = false;
            RequestsGroupBox.Enabled = false;
            ExecuteRequestButton.Enabled = false;
            UserDataTextBox.Enabled = false;
            UserData1TextBox.Enabled = false;
            UserDataTextBox.Text = string.Empty;
            UserData1TextBox.Text = string.Empty;
        }

        private void OptionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeButton.Enabled = true;

            switch (OptionsComboBox.SelectedIndex)
            {
                case 0:
                    ReaderQuery(@"select * from Athletes");
                    break;
                case 1:
                    ReaderQuery(@"select * from KindOfSports");
                    break;
                case 2:
                    ReaderQuery(@"select * from OlympicsInfo");
                    break;
                case 3:
                    ReaderQuery(@"select * from KindOfSportsAndAthletes");
                    break;
                case 4:
                    ReaderQuery(@"select * from OlympicsOverallInfo");
                    break;
            }

            OlympicsDataGridView.DataSource = dt;
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

            ReaderQuery(fillDGV);

            OlympicsDataGridView.DataSource = dt;

            conn?.Close();
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
            dt = new DataTable();

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
                                                                 order by sum(A.GoldMedal) desc", conn);
                        break;
                    case 1:
                        adapter.SelectCommand = new SqlCommand(@"select A.Name + ' ' + A.Surname as [Full Name], KS.Name as [Kind of Sport], sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 group by A.Name + ' ' + A.Surname, KS.Name
                                                                 order by KS.Name", conn);
                        break;
                    case 2:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) A.Country, max(A.GoldMedal) as [Most Gold Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 group by A.Country
                                                                 order by max(A.GoldMedal) desc", conn);
                        break;
                    case 3:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) HostCountryName as [Host Country], count(HostCountryName) as Amount
                                                                 from OlympicsInfo 
                                                                 group by HostCountryName
                                                                 order by count(HostCountryName) desc", conn);
                        break;
                    case 4:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and OI.Id = @id
                                                                 group by  A.Country
                                                                 order by sum(A.GoldMedal) desc", conn);
                        break;
                    case 5:
                        adapter.SelectCommand = new SqlCommand(@"select A.Name + ' ' + A.Surname as [Full Name], KS.Name as [Kind of Sport], sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and OI.Id = @id
                                                                 group by A.Name + ' ' + A.Surname, KS.Name
                                                                 order by KS.Name", conn);
                        break;
                    case 6:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) A.Country, max(A.GoldMedal) as [Most Gold Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and OI.Id = @id
                                                                 group by A.Country
                                                                 order by max(A.GoldMedal) desc", conn);
                        break;
                    case 7:
                        adapter.SelectCommand = new SqlCommand(@"select top(1) A.Name + ' ' + A.Surname as [Full Name], sum(A.GoldMedal) as [Most Gold Medals]
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and KS.Name = @kind
                                                                 group by A.Name + ' ' + A.Surname
                                                                 order by sum(A.GoldMedal) desc", conn);

                        adapter.SelectCommand.Parameters.Add("@kind",SqlDbType.VarChar).Value = UserDataTextBox.Text.ToString();
                        break;
                    case 8:
                        adapter.SelectCommand = new SqlCommand(@"select Name + ' ' + Surname as [Full Name]
                                                                 from Athletes
                                                                 where Country = @country", conn);

                        break;
                    case 9:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals], sum(KSA.Result) as Result
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and A.Country = @country
                                                                 group by A.Country
                                                                 order by sum(KSA.Result) desc", conn);
                        break;
                    case 10:
                        adapter.SelectCommand = new SqlCommand(@"select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals], sum(KSA.Result) as Result
                                                                 from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                                 where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                                 and A.Country = @country and OI.Id = @id
                                                                 group by A.Country
                                                                 order by sum(KSA.Result) desc", conn);

                        break;
                }

                if((index > 3 && index < 7) || index == 10) adapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = Int32.TryParse( index == 10 ? UserData1TextBox.Text.ToString() : UserDataTextBox.Text.ToString(), out int temp) && (temp > 0 && temp < 3) ? temp : throw new Exception();
                if(index > 7) adapter.SelectCommand.Parameters.Add("@country", SqlDbType.VarChar).Value = UserDataTextBox.Text.ToString();

                adapter.Fill(dt);
                OlympicsDataGridView.DataSource = dt;

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
