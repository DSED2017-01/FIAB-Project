using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Microsoft.Office.Interop.Excel;

using ExtractExcel.Lib;
using ExtractExcel.LIB.DAO;
using ExtractExcel.Lib.DAO;

namespace ExtractExcel
{
    public class OrderListExtractor : BaseExcelExtractor
    {

        public override void ProcessWorksheet(Range range)
        {

            string[] group_list = { "DISCUS", "LOACH", "PUFFERS", "INVERTEBRATES", "Wild FISH",
                            "CATFISH", "BARBS", "ORTHER FISHES", "TETRAS", "GOURAMI", "GUPPIES",
                            "PLATIES", "SWORDtailS", "MOLLIES", "CICHLIDS",  "ANGELS", "GOLD FISH" };

            StringBuilder line;
            List<string> size = new List<string>();
            List<string> group = new List<string>();

            string text;
            //int count = 0;
            int len02 = 1;
            int len03 = 1;

            string group_name = string.Empty;
            string common_name = string.Empty;
            string scientific_name = string.Empty;
            string description = string.Empty;

            int pet_size_id = -1;
            int group_id = -1;
            for (int row = 1; row < rowCount; row++)
            {
                line = new StringBuilder();
                for (int column = 1; column < 10; column++) // colCount; column++)
                {
                    if (range.Cells[row, column] != null && range.Cells[row, column].Value != null)
                    {
                        text = range.Cells[row, column].Value.ToString().Trim();

                        //Console.Write(range.Cells[row, column].Value.ToString() + "|");
                    }
                    else
                    {
                        text = string.Empty;
                        //Console.Write("\t\t|");
                    }

                    switch(column)
                    {
                        /*  Code field*/
                        case 1:
                            string temp = text;
                            text = temp.PadRight(10, ' ');
                            break;
                        case 2:
                            if (text.Length > len02)
                                len02 = text.Length;
                            temp = text;
                            //text = temp.PadRight(len02, ' ');
                            break;
                        case 3:
                            if (text.Length > len03)
                                len03 = text.Length;
                            temp = text;
                            //text = temp.PadRight(len03, ' ');
                            break;
                        case 5:
                            //bool not_found = size.Contains(text.Trim()) ? false : true ;
                            if ( text.Trim() != string.Empty && text.Trim() != "Size")
                            {
                                if( size.Count ==0 || CheckForWord(text.Trim(), size.ToArray()) != true )
                                {
                                    size.Add(text.Trim());
                                    pet_size_id = ProcessRECORD_PET_SIZE(text.Trim());
                                }
                                    
                            }               
                            break;
                        default: break;
                    }
                    if(text.Trim() != string.Empty && column != 1 && column < 4)
                    {
                        line.Append(text + " |");
                    }
                    else
                    {
                        line.Append(text + "|");
                    }


                }

                //Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}");

                string[] str = line.ToString().Split('|');


                //Console.Write(str[0] +"|"+str[1]+"|"+str[2] +"|");
                Console.Write(str[0] + "|");

                /* Extract the Group Information and pre-format it*/

                //group_name = (str.Length > 2) ? str[1].Trim() : "";
                if (str[0].Trim() == string.Empty)
                {
                    group_name = (str.Length > 2) ? str[1].Trim() : "";
                    if (group_name != string.Empty)
                    {

                        group_name = group_name[0] + group_name.Substring(1).ToLower();

                        /* convert the group name from pural to singular */
                        if (group_name.Contains("ies"))
                        {
                            group_name = group_name.Substring(0, group_name.Length - "ies".Length) + "y";
                        }

                        /* only add the group name ONCE to the list*/
                        if (group.Contains(group_name) != true)
                        {
                            group.Add(group_name);
                            group_id = ProcessRECORD_GROUP(group_name);
                            //Console.Write(group_name);
                        }

                    }
                }
                /* ignore line contains the following words */
                else if (str[1].Trim() == "Scientific Name")
                {

                }
                /* extract scientific name */
                else if (str[1].Trim() != string.Empty )
                {
                    //string code = str[0].Trim();
                    RECORD_PET data = new RECORD_PET();

                    data.CODE = str[0].Trim();
                    data.GROUP_FK = group_id;
                    data.SIZE_FK = pet_size_id;

                    string[] split_text = str[1].Split(' ');
                    /* Only use the first 2 words as the scientfic name */
                    scientific_name = split_text[0] + " " + split_text[1];
                    int record_id = SpeciesDataHelper.GetIDByScientificName(scientific_name);
                    data.SPECIES_FK = record_id;

                    if (record_id > 0) /* if record found */
                    {
                        Console.Write("Record Found|");
                        description = str[1].Substring(scientific_name.Length).Trim();
                        if(description != string.Empty)
                        {
                            Console.Write($"  *{group_name} " + description);
                            data.DESCRIPTION = group_name + " " + description;
                        }
                        else
                        {
                            //Console.Write($"  ={scientific_name}");
                            description = str[2].Trim();
                            if (description != string.Empty)
                            {
                                Console.Write("  =" + description);
                                data.DESCRIPTION = description;
                            }
                            else
                            {
                                data.DESCRIPTION = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        Console.Write(" Not Found! |");
                        description = str[2].Trim();
                        if (description != string.Empty)
                        {
                            Console.Write("  +" +description);
                            data.DESCRIPTION = description;
                        }
                        else
                        {
                            data.DESCRIPTION = string.Empty;
                        }
                    }
                    processRECORD_PET(data);
                }




                Console.Write("\n");

                // Debug
                if (row == 685) break;
                //if (row == 200) break;
            }

            Console.WriteLine();
            Console.WriteLine("Group :");
            if (group.Count > 0)
                for (int i = 0; i < group.Count; i++)
                    Console.WriteLine($"  {(i + 1).ToString().PadLeft(2, '0')} [" + group[i] + "]");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Size :");
            if (size.Count> 0)
                for(int i=0; i < size.Count;i++)
                    Console.WriteLine($"  {(i+1).ToString().PadLeft(2,'0')} [" + size[i] + "]");
            Console.WriteLine();
            
        }

        private int ProcessRECORD_GROUP(string selected_field)
        {
            /*
                CREATE TABLE [dbo].[RECORD_GROUP] (
                    [ID_PK]       INT           IDENTITY (1, 1) NOT NULL,
                    [DESCRIPTION] NVARCHAR (25) NOT NULL,
                    PRIMARY KEY CLUSTERED ([ID_PK] ASC)
                );
             */
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [RECORD_GROUP] WHERE [DESCRIPTION] = @FIELD_TEXT";
            command.Parameters.AddWithValue("@FIELD_TEXT", selected_field);

            int field_id = DAOHelper.RetreiveID(command);

            SqlCommand executeDataCommand = new SqlCommand();
            executeDataCommand.Parameters.AddWithValue("@FIELD_TEXT", selected_field);

            if (field_id == -1) /* New Record */
            {
                executeDataCommand.CommandText = "INSERT INTO [RECORD_GROUP] ([DESCRIPTION]) VALUES(@FIELD_TEXT) ;";
                DAOHelper.InsertData(executeDataCommand);

                field_id = DAOHelper.RetreiveID(command);
            }
            else
            {
                executeDataCommand.CommandText = "UPDATE [RECORD_GROUP]  SET [DESCRIPTION] = @FIELD_TEXT WHERE [ID_PK] = @ID_PK ;";
                executeDataCommand.Parameters.AddWithValue("@ID_PK", field_id);
                DAOHelper.InsertData(executeDataCommand);
            }
            return field_id;
        }

        private int ProcessRECORD_PET_SIZE(string selected_field)
        {
            /*
                CREATE TABLE [dbo].[RECORD_PET_SIZE] (
                    [ID_PK]       INT           IDENTITY (1, 1) NOT NULL,
                    [DESCRIPTION] NVARCHAR (25) NOT NULL,
                    PRIMARY KEY CLUSTERED ([ID_PK] ASC)
                );
             */
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [RECORD_PET_SIZE] WHERE [DESCRIPTION] = @FIELD_TEXT";
            command.Parameters.AddWithValue("@FIELD_TEXT", selected_field);

            int field_id = DAOHelper.RetreiveID(command);

            SqlCommand executeDataCommand = new SqlCommand();
            executeDataCommand.Parameters.AddWithValue("@FIELD_TEXT", selected_field);

            if (field_id == -1) /* New Record */
            {
                executeDataCommand.CommandText = "INSERT INTO [RECORD_PET_SIZE] ([DESCRIPTION]) VALUES(@FIELD_TEXT) ;";
                DAOHelper.InsertData(executeDataCommand);

                field_id = DAOHelper.RetreiveID(command);
            }
            else
            {
                executeDataCommand.CommandText = "UPDATE [RECORD_PET_SIZE]  SET [DESCRIPTION] = @FIELD_TEXT WHERE [ID_PK] = @ID_PK ;";
                executeDataCommand.Parameters.AddWithValue("@ID_PK", field_id);
                DAOHelper.InsertData(executeDataCommand);
            }
            return field_id;
        }

        class RECORD_PET
        {
            //public int ID_PK { set; get; }
            public string CODE { set; get; }
            public string DESCRIPTION { set; get; }
            public int SPECIES_FK { set; get; } 
            public int SIZE_FK { set; get; }
            public int GROUP_FK { set; get; }
        }

        private void processRECORD_PET(RECORD_PET data)
        {
            /* 
                CREATE TABLE [dbo].[RECORD_PET] (
                    [ID_PK]       INT           IDENTITY (1, 1) NOT NULL,
                    [CODE]        NVARCHAR (10) NOT NULL,
                    [DESCRIPTION] NVARCHAR (50) NULL,
                    [SPECIES_FK]  INT           NULL,
                    [SIZE_FK]     INT           NOT NULL,
                    [GROUP_FK]    INT           NULL, 
                    PRIMARY KEY CLUSTERED ([ID_PK] ASC)
                );
             */
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [RECORD_PET] WHERE [CODE] = @CODE_FIELD";
            command.Parameters.AddWithValue("@CODE_FIELD", data.CODE);

            int field_id = DAOHelper.RetreiveID(command);

            SqlCommand executeDataCommand = new SqlCommand();
            executeDataCommand.Parameters.AddWithValue("@CODE_FIELD", data.CODE);

            if (field_id == -1) /* New Record */
            {
                //executeDataCommand.CommandText = "INSERT INTO [RECORD_PET] ([CODE]) VALUES(@CODE_FIELD) ;";

                if(data.SPECIES_FK > 0)
                {
                    executeDataCommand.CommandText = "INSERT INTO [RECORD_PET]"+
                        " ([CODE], [DESCRIPTION], [SPECIES_FK], [SIZE_FK], [GROUP_FK])" +
                        " VALUES(@CODE_FIELD, @DESCRIPTION_FIELD, @SPECIES_FIELD, @SIZE_FIELD, @GROUP_FIELD) ;";
                    executeDataCommand.Parameters.AddWithValue("@SPECIES_FIELD", data.SPECIES_FK);
                }
                else
                {
                    executeDataCommand.CommandText = "INSERT INTO [RECORD_PET]" +
                        " ([CODE], [DESCRIPTION], [SIZE_FK], [GROUP_FK])" +
                        " VALUES(@CODE_FIELD, @DESCRIPTION_FIELD, @SIZE_FIELD, @GROUP_FIELD) ;";
                }
                executeDataCommand.Parameters.AddWithValue("@DESCRIPTION_FIELD", data.DESCRIPTION);
                executeDataCommand.Parameters.AddWithValue("@SIZE_FIELD", data.SIZE_FK);
                executeDataCommand.Parameters.AddWithValue("@GROUP_FIELD", data.GROUP_FK);
                DAOHelper.InsertData(executeDataCommand);

                //field_id = DAOHelper.RetreiveID(command);
            }
            else
            {
                //executeDataCommand.CommandText = "UPDATE [RECORD_PET_SIZE]  SET [CODE] = @CODE_FIELD" + "WHERE [ID_PK] = @ID_PK ;";
                executeDataCommand.Parameters.AddWithValue("@ID_PK", field_id);
                if (data.SPECIES_FK > 0)
                {
                    executeDataCommand.CommandText = "UPDATE [RECORD_PET] " 
                        + " SET [CODE] = @CODE_FIELD, [DESCRIPTION] = @DESCRIPTION_FIELD, [SPECIES_FK] = @SPECIES_FIELD, [SIZE_FK] = @SIZE_FIELD, [GROUP_FK] = @GROUP_FIELD"
                        + " WHERE [ID_PK] = @ID_PK ;";
                    executeDataCommand.Parameters.AddWithValue("@SPECIES_FIELD", data.SPECIES_FK);
                }
                else
                {
                    executeDataCommand.CommandText = "UPDATE [RECORD_PET] "
                        + " SET [CODE] = @CODE_FIELD, [DESCRIPTION] = @DESCRIPTION_FIELD, [SIZE_FK] = @SIZE_FIELD, [GROUP_FK] = @GROUP_FIELD"
                        + " WHERE [ID_PK] = @ID_PK ;";
                }
                executeDataCommand.Parameters.AddWithValue("@DESCRIPTION_FIELD", data.DESCRIPTION);
                executeDataCommand.Parameters.AddWithValue("@SIZE_FIELD", data.SIZE_FK);
                executeDataCommand.Parameters.AddWithValue("@GROUP_FIELD", data.GROUP_FK);
                DAOHelper.InsertData(executeDataCommand);
            }
        }
    }
}
