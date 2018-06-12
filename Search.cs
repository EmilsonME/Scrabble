using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Iskrabol{

	class Search{
		private OleDbConnection con;
		private string connstr, desc;
		private OleDbCommand cmd, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7;
		private OleDbDataReader dr,dr2,dr3,dr4,dr5,dr6,dr7;
		private int limit, counter;
		private List<string> wordsGet = new List<string>();


		string query0 = "SELECT Word FROM ";
		string query1 = " WHERE Word = '";

		public bool getValid = false;

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
			string sub2 = "zzz";

		public string getDropboxPath() {
			var infoPath = @"Dropbox\info.json";
			var jsonPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), infoPath);            
			if (!File.Exists(jsonPath)) jsonPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), infoPath);

			if (!File.Exists(jsonPath)) throw new Exception("Dropbox could not be found!");

			var dropboxPath = File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\");

			return dropboxPath;
		}

		public List<string> searchForValidity(List<string> str){
			connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=IskrabolDictionary.accdb";

			List<string> arrTemp = new List<string>();
			List<string> arrTemp2 = new List<string>();


						

			try
			{
				con = new OleDbConnection(connstr);
				cmd = new OleDbCommand();
				con.Open();

	 			for(int ctr = 0; ctr < str.Count; ctr++){			
	 					cmd.Connection = con;

						if(str[ctr].StartsWith("A")){
						 	cmd.CommandText = query0 + AWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'B'){
						 	cmd.CommandText = query0 + BWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'C'){
						 	cmd.CommandText = query0 + CWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'D'){
						 	cmd.CommandText = query0 + DWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'E'){
						 	cmd.CommandText = query0 + EWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'F'){
						 	cmd.CommandText = query0 + FWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'G'){
						 	cmd.CommandText = query0 + GWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'H'){
						 	cmd.CommandText = query0 + HWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'I'){
						 	cmd.CommandText = query0 + IWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'J'){
						 	cmd.CommandText = query0 + JWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'K'){
						 	cmd.CommandText = query0 + KWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'L'){
						 	cmd.CommandText = query0 + LWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'M'){
						 	cmd.CommandText = query0 + MWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'N'){
						 	cmd.CommandText = query0 + NWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'O'){
						 	cmd.CommandText = query0 + OWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'P'){
						 	cmd.CommandText = query0 + PWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'Q'){
						 	cmd.CommandText = query0 + QWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'R'){
						 	cmd.CommandText = query0 + RWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'S'){
						 	cmd.CommandText = query0 + SWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'T'){
						 	cmd.CommandText = query0 + TWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'U'){
						 	cmd.CommandText = query0 + UWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'V'){
						 	cmd.CommandText = query0 + VWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'W'){
						 	cmd.CommandText = query0 + WWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'X'){
						 	cmd.CommandText = query0 + XWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'Y'){
						 	cmd.CommandText = query0 + YWords + query1 + str[ctr] + "'";
						}else if(str[ctr][0] == 'Z'){
						 	cmd.CommandText = query0 + ZWords + query1 + str[ctr] + "'";
						}


						string temp = Convert.ToString(cmd.ExecuteScalar());
						if(temp.Equals(str[ctr])) {
							Console.WriteLine("Valid word: " + temp);
							arrTemp.Add(str[ctr]);
						}else{
							Console.WriteLine("Invalid word: " + str[ctr] );
							arrTemp2.Add(str[ctr]);
					}
				}
				

			}catch(Exception err){
				 MessageBox.Show(err.ToString());
			}finally{
				con.Close();
			}


			if(getValid == false){
				return arrTemp2;

			}else{
				getValid= false;
				return arrTemp;
			}

		}
		public List<string>[] findPattern(List<string> sub, int limit, string desc){

			connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=IskrabolDictionary.accdb";


			Console.WriteLine("subs" + string.Join(",", sub) +" limit:" + limit);
			List<string>[] all= new List<string>[7];
			for(int x = 0; x < 7; x++){
				all[x] = new List<string>();
			}
		

			try{
				con = new OleDbConnection(connstr);
				cmd = new OleDbCommand();
				cmd2 = new OleDbCommand();
				cmd3= new OleDbCommand();
				cmd4 = new OleDbCommand();
				cmd5= new OleDbCommand();
				cmd6 = new OleDbCommand();
				cmd7 = new OleDbCommand();
				

				con.Open();
				cmd.Connection = con;
				cmd2.Connection = con;
				cmd3.Connection = con;
				cmd4.Connection = con;
				cmd5.Connection = con;
				cmd6.Connection = con;
				cmd7.Connection = con;

				Thread t1 =	new Thread(()=> {
					try{	
						if(desc == "full"){
							cmd.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[0] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd.CommandText = "SELECT Word FROM "+ sub[0][0]+ "Words WHERE Word LIKE '" + sub[0] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd.CommandText = "SELECT Word FROM "+ sub[0][0]+ "Words WHERE Word LIKE '%" + sub[0] + "' AND LEN(Word) <= " + limit;
						} else {
							cmd.CommandText = "SELECT Word FROM ZWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= 2" ;
						}
						dr = cmd.ExecuteReader();
						
						while(dr.Read())
						{
			   			  all[0].Add(dr[0].ToString());
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});t1.Start();
				Thread t2 =	new Thread(()=> {
					try{
						if(desc == "full"){
							cmd2.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[1] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd2.CommandText = "SELECT Word FROM "+ sub[1][0]+ "Words WHERE Word LIKE '" + sub[1] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd2.CommandText = "SELECT Word FROM "+ sub[1][0]+ "Words WHERE Word LIKE '%" + sub[1] + "' AND LEN(Word) <= " + limit;
						}else {
							cmd2.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= " + limit;

						} 
						dr2 = cmd2.ExecuteReader();
						
						while(dr2.Read())
						{
			   			  all[1].Add(dr2[0].ToString()   );
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});
				t2.Start();
				Thread t3 =	new Thread(()=> {
					try{	
						if(desc == "full"){
							cmd3.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[2] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd3.CommandText ="SELECT Word FROM "+ sub[2][0]+ "Words WHERE Word LIKE '" + sub[2] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd3.CommandText = "SELECT Word FROM "+ sub[2][0]+ "Words WHERE Word LIKE '%" + sub[2] + "' AND LEN(Word) <= " + limit;
						}else {
							cmd3.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= " + limit;

						} 
						dr3 = cmd3.ExecuteReader();
						
						while(dr3.Read())
						{
			   			  all[2].Add(dr3[0].ToString()   );
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});

				t3.Start();
				Thread t4 =	new Thread(()=> {
					try{
						if(desc == "full"){
							cmd4.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[3] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd4.CommandText = "SELECT Word FROM "+ sub[3][0]+ "Words WHERE Word LIKE '" + sub[3] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd4.CommandText = "SELECT Word FROM "+ sub[3][0]+ "Words WHERE Word LIKE '%" + sub[3] + "' AND LEN(Word) <= " + limit;
						}else {
							cmd4.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= " + limit;

						} 
						dr4 = cmd4.ExecuteReader();
						
						while(dr4.Read())
						{
			   			  all[3].Add(dr4[0].ToString()   );
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});
				t4.Start();
				Thread t5 =	new Thread(()=> {
					try{
						if(desc == "full"){
							cmd5.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[4] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd5.CommandText = "SELECT Word FROM "+ sub[4][0]+ "Words WHERE Word LIKE '" + sub[4] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd5.CommandText = "SELECT Word FROM "+ sub[4][0]+ "Words WHERE Word LIKE '%" + sub[4] + "' AND LEN(Word) <= " + limit;
						}else {
							cmd5.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= " + limit;

						} 
						dr5 = cmd5.ExecuteReader();
						
						while(dr5.Read())
						{
			   			  all[4].Add(dr5[0].ToString()   );
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});
				t5.Start();
				Thread t6 =	new Thread(()=> {
					try{
						if(desc == "full"){
							cmd6.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[5] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd6.CommandText = "SELECT Word FROM "+ sub[5][0]+ "Words WHERE Word LIKE '" + sub[5] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd6.CommandText = "SELECT Word FROM "+ sub[5][0]+ "Words WHERE Word LIKE '%" + sub[5] + "' AND LEN(Word) <= " + limit;
						}else {
							cmd6.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= " + limit;

						} 
						dr6 = cmd6.ExecuteReader();
						
						while(dr6.Read())
						{
			   			  all[5].Add(dr6[0].ToString()   );
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});
				t6.Start();
				Thread t7 =	new Thread(()=> {
					try{
						if(desc == "full"){
							cmd7.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[6] + "%' AND LEN(Word) <= " + limit;
						}else if (desc == "right" || desc == "down"){
							cmd7.CommandText = "SELECT Word FROM "+ sub[6][0]+ "Words WHERE Word LIKE '" + sub[6] + "%' AND LEN(Word) <= " + limit;
						} else if (desc == "left" || desc == "top"){
							cmd7.CommandText = "SELECT Word FROM "+ sub[6][0]+ "Words WHERE Word LIKE '%" + sub[6] + "' AND LEN(Word) <= " + limit;
						}else {
							cmd7.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub2+ "' AND LEN(Word) <= " + limit;

						}
						dr7 = cmd7.ExecuteReader();
						
						while(dr7.Read())
						{
			   			  all[6].Add(dr7[0].ToString());
						}
					}catch(Exception e){
						Console.WriteLine(e.ToString());
					}
					});
					
				t7.Start();

				t1.Join();
				t2.Join();
				t3.Join();
				t4.Join();
				t5.Join();
				t6.Join();
				t7.Join();




			}catch(Exception e){
					MessageBox.Show(e.ToString());
			}


			return all;
		}
		
		

		}
			
	
}
/*	if(desc == "full"){
				     cmd.CommandText = "SELECT Word FROM AllWords WHERE Word LIKE '%" + sub[0] + "%' OR Word LIKE '%" + sub[1] + "%' OR Word LIKE '%" + sub[2] + "%' OR Word LIKE '%" + sub[3] + "%' OR Word LIKE '%" + sub[4] + "%' OR Word LIKE '%" + sub[5] + "%' OR Word LIKE '%" + sub[6] + "%' AND LEN(Word) <= " + limit;
				}else if (desc == "right" || desc == "down"){
				     cmd.CommandText = "SELECT Word FROM English WHERE Word LIKE '" + sub[0] + "%' OR Word LIKE '" + sub[1] + "%' OR Word LIKE '" + sub[2] + "%' OR Word LIKE '" + sub[3] + "%' OR Word LIKE '" + sub[4] + "%' OR Word LIKE '" + sub[5] + "%' OR Word LIKE '" + sub[6] + "%' AND LEN(Word) <= " + limit;
				} else if (desc == "left" || desc == "top"){
				     cmd.CommandText = "SELECT Word FROM English WHERE Word LIKE '%" + sub[0] + "' OR Word LIKE '%" + sub[1] + "' OR Word LIKE '%" + sub[2] + "' OR Word LIKE '%" + sub[3] + "' OR Word LIKE '%" + sub[4] + "' OR Word LIKE '%" + sub[5] + "' OR Word LIKE '%" + sub[6] + "' AND LEN(Word) <= " + limit;
				} 
					
				dr = cmd.ExecuteReader();

				*/