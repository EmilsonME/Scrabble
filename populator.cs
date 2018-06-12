using System;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

public class Popu 
{
	public static void Main()
	{
		using(StreamReader r = new StreamReader("dictionary.txt"))
		{
			OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Dropbox\Iskrabol\IskrabolDictionary.accdb");
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = con;
			string line;
			try
			{

				con.Open();
				while((line = r.ReadLine()) != null)
				{
					string [] liner = line.split(",");
					try
					{	if(line[0] == 'A'){
						 	cmd.CommandText = query0 + AWords + query1 + line + "')";
						}else if(line[0] == 'B'){
						 	cmd.CommandText = query0 + BWords + query1 + line + "')";
						}else if(line[0] == 'C'){
						 	cmd.CommandText = query0 + CWords + query1 + line + "')";
						}else if(line[0] == 'D'){
						 	cmd.CommandText = query0 + DWords + query1 + line + "')";
						}else if(line[0] == 'E'){
						 	cmd.CommandText = query0 + EWords + query1 + line + "')";
						}else if(line[0] == 'F'){
						 	cmd.CommandText = query0 + FWords + query1 + line + "')";
						}else if(line[0] == 'G'){
						 	cmd.CommandText = query0 + GWords + query1 + line + "')";
						}else if(line[0] == 'H'){
						 	cmd.CommandText = query0 + HWords + query1 + line + "')";
						}else if(line[0] == 'I'){
						 	cmd.CommandText = query0 + IWords + query1 + line + "')";
						}else if(line[0] == 'J'){
						 	cmd.CommandText = query0 + JWords + query1 + line + "')";
						}else if(line[0] == 'K'){
						 	cmd.CommandText = query0 + KWords + query1 + line + "')";
						}else if(line[0] == 'L'){
						 	cmd.CommandText = query0 + LWords + query1 + line + "')";
						}else if(line[0] == 'M'){
						 	cmd.CommandText = query0 + MWords + query1 + line + "')";
						}else if(line[0] == 'N'){
						 	cmd.CommandText = query0 + NWords + query1 + line + "')";
						}else if(line[0] == 'O'){
						 	cmd.CommandText = query0 + OWords + query1 + line + "')";
						}else if(line[0] == 'P'){
						 	cmd.CommandText = query0 + PWords + query1 + line + "')";
						}else if(line[0] == 'Q'){
						 	cmd.CommandText = query0 + QWords + query1 + line + "')";
						}else if(line[0] == 'R'){
						 	cmd.CommandText = query0 + RWords + query1 + line + "')";
						}else if(line[0] == 'S'){
						 	cmd.CommandText = query0 + SWords + query1 + line + "')";
						}else if(line[0] == 'T'){
						 	cmd.CommandText = query0 + TWords + query1 + line + "')";
						}else if(line[0] == 'U'){
						 	cmd.CommandText = query0 + UWords + query1 + line + "')";
						}else if(line[0] == 'V'){
						 	cmd.CommandText = query0 + VWords + query1 + line + "')";
						}else if(line[0] == 'W'){
						 	cmd.CommandText = query0 + WWords + query1 + line + "')";
						}else if(line[0] == 'X'){
						 	cmd.CommandText = query0 + XWords + query1 + line + "')";
						}else if(line[0] == 'Y'){
						 	cmd.CommandText = query0 + YWords + query1 + line + "')";
						}else if(line[0] == 'Z'){
						 	cmd.CommandText = query0 + ZWords + query1 + line + "')";
						}


						 cmd.ExecuteNonQuery();
					} 
					catch(Exception ex) 
					{
						Console.WriteLine(ex.Message);
					}
				}
			} 
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				con.Close();
			}
			
			MessageBox.Show("INSERT SUCCESS");
		}
	}
}