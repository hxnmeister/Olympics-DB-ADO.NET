using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Практичне_завдання_23._06._23
{
    public partial class Form1 : Form
    {
        DataTable dt = null;
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        SqlCommand cmd = null;

        public Form1()
        {
            InitializeComponent();

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectToOlympicsDB"].ConnectionString);
            dt = new DataTable();
            adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(@"select A.Name, A.Surname, A.Patronymic, A.Country as [Origin Country], A.BirthDate as [Birth Date], A.GoldMedal as [Gold Medals],
                                                     A.SilverMedal as [Silver Medals], A.BronzeMedal as [Bronze Medals], KS.Name as [Kind of Sport], KS.NumberOfParticipants as [Number of Patricipants],
                                                     OI.Season, OI.HostCountryName as [Host Country], OI.HostCityName as [Host City], OI.Year
                                                     from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                                                     where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                                                     order by A.Id", conn);

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
                        if(tableName == "Athletes")
                        {
                            cmd = new SqlCommand(@"insert into Athletes(Id, Name, Surname, Patronymic, Country, BirthDate, GoldMedal, SilverMedal, BronzeMedal)
                                                   values(@id, @name, @surname, @patronymic, @country, @birthDate, @goldMedal, @silverMedal, @bronzeMedal)", conn);

                            cmd.Parameters.Add("@id",SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@name",SqlDbType.VarChar).Value = changedRow["Name"];
                            cmd.Parameters.Add("@surname",SqlDbType.VarChar).Value = changedRow["Surname"];
                            cmd.Parameters.Add("@patronymic",SqlDbType.VarChar).Value = changedRow["Patronymic"] ?? DBNull.Value;
                            cmd.Parameters.Add("@country",SqlDbType.VarChar).Value = changedRow["Country"];
                            cmd.Parameters.Add("@birthDate",SqlDbType.Date).Value = changedRow["BirthDate"];
                            cmd.Parameters.Add("@goldMedal",SqlDbType.Int).Value = changedRow["GoldMedal"];
                            cmd.Parameters.Add("@silverMedal",SqlDbType.Int).Value = changedRow["SilverMedal"];
                            cmd.Parameters.Add("@bronzeMedal",SqlDbType.Int).Value = changedRow["BronzeMedal"];
                        }
                        else if(tableName == "KindOfSports")
                        {
                            cmd = new SqlCommand(@"insert into KindOfSports(Id, Name, NumberOfParticipants)
                                                   values(@id, @name, @numberOfParticipants)", conn);

                            cmd.Parameters.Add("@id",SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@name",SqlDbType.VarChar).Value = changedRow["Name"];
                            cmd.Parameters.Add("@numberOfParticipants", SqlDbType.VarChar).Value = changedRow["NumberOfParticipants"];
                        }
                        else if (tableName == "OlympicsInfo")
                        {
                            cmd = new SqlCommand(@"insert into OlympicsInfo(Id, Season, HostCountryName, HostCityName, Year)
                                                   values(@id, @season, @hostCountryName, @hosyCityName, @year)", conn);

                            cmd.Parameters.Add("@id",SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@season",SqlDbType.VarChar).Value = changedRow["Season"];
                            cmd.Parameters.Add("@hostCountryName", SqlDbType.VarChar).Value = changedRow["HostCountryName"];
                            cmd.Parameters.Add("@hosyCityName", SqlDbType.VarChar).Value = changedRow["HostCityName"];
                            cmd.Parameters.Add("@year", SqlDbType.Int).Value = changedRow["Year"];

                        }
                        else if (tableName == "KindOfSportsAndAthletes")
                        {
                            cmd = new SqlCommand(@"insert into KindOfSportsAndAthletes(Id, KindOfSportId, AthleteId, Result)
                                                   values(@id, @kindOfSportId, @athleteId, @result)", conn);

                            cmd.Parameters.Add("@id",SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@kindOfSportId", SqlDbType.Int).Value = changedRow["KindOfSportId"];
                            cmd.Parameters.Add("@athleteId", SqlDbType.Int).Value = changedRow["AthleteId"];
                            cmd.Parameters.Add("@result", SqlDbType.Float).Value = changedRow["Result"];

                        }
                        else if (tableName == "OlympicsOverallInfo")
                        {
                            cmd = new SqlCommand(@"insert into OlympicsOverallInfo(Id, OlympicsInfoId, KindOfSportAndAthleteId)
                                                   values(@id, @olympicsInfoId, @kindOfSportAndAthleteId)", conn);

                            cmd.Parameters.Add("@id",SqlDbType.Int).Value = changedRow["Id"];
                            cmd.Parameters.Add("@olympicsInfoId", SqlDbType.Int).Value = changedRow["OlympicsInfoId"];
                            cmd.Parameters.Add("@kindOfSportAndAthleteId", SqlDbType.Int).Value = changedRow["KindOfSportAndAthleteId"];

                        }
                    }
                    else if(changedRow.RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand($"delete from {tableName} where Id = @id",conn);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(changedRow["Id", DataRowVersion.Original]);
                    }
                    else if(changedRow.RowState == DataRowState.Modified)
                    {
                        if (tableName == "Athletes")
                        {
                            cmd = new SqlCommand(@"update Athletes set Name = @name, Surname = @surname, Patronymic = @patronymic, 
                                                   Country = @country, BirthDate = @birthDate, GoldMedal = @goldMedal, 
                                                   SilverMedal = @silverMedal, BronzeMedal = @bronzeMedal where Id = @id", conn);

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
                            cmd = new SqlCommand(@"update OlympicsOverallInfo set OlympicsInfoId = @olympicsInfoId, KindOfSportAndAthleteId = @kindOfSportAndAthleteId,
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
            EditorModeExitButton.Enabled = false;
            OlympicsDataGridView.ReadOnly = true;

            ReaderQuery(@"select A.Name, A.Surname, A.Patronymic, A.Country as [Origin Country], A.BirthDate as [Birth Date], A.GoldMedal as [Gold Medals],
                          A.SilverMedal as [Silver Medals], A.BronzeMedal as [Bronze Medals], KSA.Result, KS.Name as [Kind of Sport], KS.NumberOfParticipants as [Number of Patricipants],
                          OI.Season, OI.HostCountryName as [Host Country], OI.HostCityName as [Host City], OI.Year
                          from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
                          where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
                          order by A.Id");
            OlympicsDataGridView.DataSource = dt;

            conn?.Close();
        }
    }
}
