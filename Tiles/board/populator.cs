using System;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

public class Popu 
{
	public static void Main()
	{
		using(StreamReader r = new StreamReader("words.txt"))
		{
			OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Dropbox\Iskrabol\IskrabolDictionary.accdb");
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = con;

			string line;
			string query0= "INSERT into ";
			string query1 = "(Word) values ('";
			string AWords = "AWords";
			string BWords = "BWords";
			string CWords = "CWords";
			string DWords = "DWords";
			string EWords = "EWords";
			string FWords = "FWords";
			string GWords = "GWords";
			string HWords = "HWords";
			string IWords = "IWords";
			string JWords = "JWords";

			string KWords = "KWords";
			string LWords = "LWords";
			string MWords = "MWords";
			string NWords = "NWords";
			string OWords = "OWords";
			string PWords = "PWords";
			string QWords = "QWords";
			string RWords = "RWords";
			string SWords = "SWords";
			string TWords = "TWords";
			string UWords = "UWords";
			string VWords = "VWords";
			string WWords = "WWords";
			string XWords = "XWords";
			string YWords = "YWords";
			string ZWords = "ZWords";


			
			try
			{
				con.Open();
				while((line = r.ReadLine()) != null)
				{
					try
					{	if(line[0] == 'a'){
						 	cmd.CommandText = query0 + AWords + query1 + line + "')";
						}else if(line[0] == 'b'){
						 	cmd.CommandText = query0 + BWords + query1 + line + "')";
						}else if(line[0] == 'c'){
						 	cmd.CommandText = query0 + CWords + query1 + line + "')";
						}else if(line[0] == 'd'){
						 	cmd.CommandText = query0 + DWords + query1 + line + "')";
						}else if(line[0] == 'e'){
						 	cmd.CommandText = query0 + EWords + query1 + line + "')";
						}else if(line[0] == 'f'){
						 	cmd.CommandText = query0 + FWords + query1 + line + "')";
						}else if(line[0] == 'g'){
						 	cmd.CommandText = query0 + GWords + query1 + line + "')";
						}else if(line[0] == 'h'){
						 	cmd.CommandText = query0 + HWords + query1 + line + "')";
						}else if(line[0] == 'i'){
						 	cmd.CommandText = query0 + IWords + query1 + line + "')";
						}else if(line[0] == 'j'){
						 	cmd.CommandText = query0 + JWords + query1 + line + "')";
						}else if(line[0] == 'k'){
						 	cmd.CommandText = query0 + KWords + query1 + line + "')";
						}else if(line[0] == 'l'){
						 	cmd.CommandText = query0 + LWords + query1 + line + "')";
						}else if(line[0] == 'm'){
						 	cmd.CommandText = query0 + MWords + query1 + line + "')";
						}else if(line[0] == 'n'){
						 	cmd.CommandText = query0 + NWords + query1 + line + "')";
						}else if(line[0] == 'o'){
						 	cmd.CommandText = query0 + OWords + query1 + line + "')";
						}else if(line[0] == 'p'){
						 	cmd.CommandText = query0 + PWords + query1 + line + "')";
						}else if(line[0] == 'q'){
						 	cmd.CommandText = query0 + QWords + query1 + line + "')";
						}else if(line[0] == 'r'){
						 	cmd.CommandText = query0 + RWords + query1 + line + "')";
						}else if(line[0] == 's'){
						 	cmd.CommandText = query0 + SWords + query1 + line + "')";
						}else if(line[0] == 't'){
						 	cmd.CommandText = query0 + TWords + query1 + line + "')";
						}else if(line[0] == 'u'){
						 	cmd.CommandText = query0 + UWords + query1 + line + "')";
						}else if(line[0] == 'v'){
						 	cmd.CommandText = query0 + VWords + query1 + line + "')";
						}else if(line[0] == 'w'){
						 	cmd.CommandText = query0 + WWords + query1 + line + "')";
						}else if(line[0] == 'x'){
						 	cmd.CommandText = query0 + XWords + query1 + line + "')";
						}else if(line[0] == 'y'){
						 	cmd.CommandText = query0 + YWords + query1 + line + "')";
						}else if(line[0] == 'z'){
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