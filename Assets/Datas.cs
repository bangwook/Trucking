
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSONMetaSheets;
using System.Linq;

public class Datas_Loader : MonoBehaviour{
	public static void Load(GameObject go, int idx, string label, IEnumerator ieNumerator){
		Datas_Loader cmp = go.AddComponent<Datas_Loader>();
		cmp.label = label; cmp.idx = idx;
		cmp.StartCoroutine( ieNumerator );
	}
	public string label;
	public int idx;
}
public class Datas{
	//Document URL: https://spreadsheets.google.com/feeds/worksheets/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/public/basic?alt=json-in-script

	//Sheet SheetBaseData
	public static DatasTypes.SheetBaseData baseData = new DatasTypes.SheetBaseData();
	//Sheet SheetRewardData
	public static DatasTypes.SheetRewardData rewardData = new DatasTypes.SheetRewardData();
	//Sheet SheetFactorTypeData
	public static DatasTypes.SheetFactorTypeData factorTypeData = new DatasTypes.SheetFactorTypeData();
	//Sheet SheetQuestData
	public static DatasTypes.SheetQuestData questData = new DatasTypes.SheetQuestData();
	//Sheet SheetGuideQuestData
	public static DatasTypes.SheetGuideQuestData guideQuestData = new DatasTypes.SheetGuideQuestData();
	//Sheet SheetAchievementData
	public static DatasTypes.SheetAchievementData achievementData = new DatasTypes.SheetAchievementData();
	//Sheet SheetLevelMission
	public static DatasTypes.SheetLevelMission levelMission = new DatasTypes.SheetLevelMission();
	//Sheet SheetLevelMissionData
	public static DatasTypes.SheetLevelMissionData levelMissionData = new DatasTypes.SheetLevelMissionData();
	//Sheet SheetNewDailyMission
	public static DatasTypes.SheetNewDailyMission newDailyMission = new DatasTypes.SheetNewDailyMission();
	//Sheet SheetDays5EventData
	public static DatasTypes.SheetDays5EventData days5EventData = new DatasTypes.SheetDays5EventData();
	//Sheet SheetLevelData
	public static DatasTypes.SheetLevelData levelData = new DatasTypes.SheetLevelData();
	//Sheet SheetCargoData
	public static DatasTypes.SheetCargoData cargoData = new DatasTypes.SheetCargoData();
	//Sheet SheetCargoLevelData
	public static DatasTypes.SheetCargoLevelData cargoLevelData = new DatasTypes.SheetCargoLevelData();
	//Sheet SheetCargoRewardData
	public static DatasTypes.SheetCargoRewardData cargoRewardData = new DatasTypes.SheetCargoRewardData();
	//Sheet SheetCityData
	public static DatasTypes.SheetCityData cityData = new DatasTypes.SheetCityData();
	//Sheet SheetJoblistExpansion
	public static DatasTypes.SheetJoblistExpansion joblistExpansion = new DatasTypes.SheetJoblistExpansion();
	//Sheet SheetPartsProduction
	public static DatasTypes.SheetPartsProduction partsProduction = new DatasTypes.SheetPartsProduction();
	//Sheet SheetLevelMag
	public static DatasTypes.SheetLevelMag levelMag = new DatasTypes.SheetLevelMag();
	//Sheet SheetTruckData
	public static DatasTypes.SheetTruckData truckData = new DatasTypes.SheetTruckData();
	//Sheet SheetBuffData
	public static DatasTypes.SheetBuffData buffData = new DatasTypes.SheetBuffData();
	//Sheet SheetMarketList
	public static DatasTypes.SheetMarketList marketList = new DatasTypes.SheetMarketList();
	//Sheet SheetIAPData
	public static DatasTypes.SheetIAPData iAPData = new DatasTypes.SheetIAPData();
	//Sheet SheetCoinShopData
	public static DatasTypes.SheetCoinShopData coinShopData = new DatasTypes.SheetCoinShopData();
	//Sheet SheetEventPackData
	public static DatasTypes.SheetEventPackData eventPackData = new DatasTypes.SheetEventPackData();
	//Sheet SheetRoadCostData
	public static DatasTypes.SheetRoadCostData roadCostData = new DatasTypes.SheetRoadCostData();
	//Sheet SheetRandomBox
	public static DatasTypes.SheetRandomBox randomBox = new DatasTypes.SheetRandomBox();
	//Sheet SheetCrate
	public static DatasTypes.SheetCrate crate = new DatasTypes.SheetCrate();
	//Sheet SheetLuckyBoxData
	public static DatasTypes.SheetLuckyBoxData luckyBoxData = new DatasTypes.SheetLuckyBoxData();
	//Sheet SheetTouchObjectData
	public static DatasTypes.SheetTouchObjectData touchObjectData = new DatasTypes.SheetTouchObjectData();
	static Datas(){
		//Static constructor that initialises each sheet data
		baseData.Init(); rewardData.Init(); factorTypeData.Init(); questData.Init(); guideQuestData.Init(); achievementData.Init(); levelMission.Init(); levelMissionData.Init(); newDailyMission.Init(); days5EventData.Init(); levelData.Init(); cargoData.Init(); cargoLevelData.Init(); cargoRewardData.Init(); cityData.Init(); joblistExpansion.Init(); partsProduction.Init(); levelMag.Init(); truckData.Init(); buffData.Init(); marketList.Init(); iAPData.Init(); coinShopData.Init(); eventPackData.Init(); roadCostData.Init(); randomBox.Init(); crate.Init(); luckyBoxData.Init(); touchObjectData.Init(); 
	}
	public static int countLoaded = 0;
	public static int countLoadedMax = 29;
	public static void Reload(System.Action onCallLoaded){
		baseData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		rewardData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		factorTypeData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		questData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		guideQuestData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		achievementData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		levelMission.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		levelMissionData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		newDailyMission.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		days5EventData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		levelData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		cargoData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		cargoLevelData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		cargoRewardData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		cityData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		joblistExpansion.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		partsProduction.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		levelMag.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		truckData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		buffData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		marketList.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		iAPData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		coinShopData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		eventPackData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		roadCostData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		randomBox.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		crate.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		luckyBoxData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
		touchObjectData.Reload(()=>{countLoaded++;if(countLoaded==29 && onCallLoaded != null){onCallLoaded();}});
	}
}


namespace DatasTypes{
	public class BaseData{
		public int id;
		public float mile;
		public float fuel_efficiency;
		public float transit_skip;
		public float charge_time;
		public float charge_gold;
		public float charge_cash;
		public int[] start_cg;
		public int[] tutorial_end_cg;
		public int complete_popup_rate;
		public int g_to_v_exchange;
		public int freecash_count;
		public int delivery_boost_ad;

		public BaseData(){}

		public BaseData(int id, float mile, float fuel_efficiency, float transit_skip, float charge_time, float charge_gold, float charge_cash, int[] start_cg, int[] tutorial_end_cg, int complete_popup_rate, int g_to_v_exchange, int freecash_count, int delivery_boost_ad){
			this.id = id;
			this.mile = mile;
			this.fuel_efficiency = fuel_efficiency;
			this.transit_skip = transit_skip;
			this.charge_time = charge_time;
			this.charge_gold = charge_gold;
			this.charge_cash = charge_cash;
			this.start_cg = start_cg;
			this.tutorial_end_cg = tutorial_end_cg;
			this.complete_popup_rate = complete_popup_rate;
			this.g_to_v_exchange = g_to_v_exchange;
			this.freecash_count = freecash_count;
			this.delivery_boost_ad = delivery_boost_ad;
		}
	}
	public class SheetBaseData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,14);
		public readonly string[] labels = new string[]{"id","float mile","float fuel_efficiency","float transit_skip","float charge_time","float charge_gold","float charge_cash","int[] start_cg","int[] tutorial_end_cg","complete_popup_rate","g_to_v_exchange","freecash_count","Delivery_boost_ad"};
		private BaseData[] _rows = new BaseData[1];
		public void Init() {
			_rows = new BaseData[]{
					new BaseData(1,150f,0.8f,1f,15f,1.5f,0.001f,new int[]{10,10000},new int[]{100,10000},0,300,2,20)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetBaseData t;
			public SheetEnumerator(SheetBaseData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public BaseData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public BaseData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public BaseData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 0, "baseData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/okj9yp0/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<BaseData> _rowsList = new List<BaseData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new BaseData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.BaseData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 13; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'mile' OF TYPE 'float'
				float tmp1; _rows[i].mile = float.TryParse( columns[1], out tmp1) ? tmp1 : 0f;
				//Variable 'fuel_efficiency' OF TYPE 'float'
				float tmp2; _rows[i].fuel_efficiency = float.TryParse( columns[2], out tmp2) ? tmp2 : 0f;
				//Variable 'transit_skip' OF TYPE 'float'
				float tmp3; _rows[i].transit_skip = float.TryParse( columns[3], out tmp3) ? tmp3 : 0f;
				//Variable 'charge_time' OF TYPE 'float'
				float tmp4; _rows[i].charge_time = float.TryParse( columns[4], out tmp4) ? tmp4 : 0f;
				//Variable 'charge_gold' OF TYPE 'float'
				float tmp5; _rows[i].charge_gold = float.TryParse( columns[5], out tmp5) ? tmp5 : 0f;
				//Variable 'charge_cash' OF TYPE 'float'
				float tmp6; _rows[i].charge_cash = float.TryParse( columns[6], out tmp6) ? tmp6 : 0f;
				//Variable 'start_cg' OF TYPE 'int[]'
				columns[7] = CleanString(columns[7]);	
				string[] tmp7 = columns[7].Split(',');
				_rows[i].start_cg =  (columns[7] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp7, ParseInt);
				//Variable 'tutorial_end_cg' OF TYPE 'int[]'
				columns[8] = CleanString(columns[8]);	
				string[] tmp8 = columns[8].Split(',');
				_rows[i].tutorial_end_cg =  (columns[8] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp8, ParseInt);
				//Variable 'complete_popup_rate' OF TYPE 'int'
				int tmp9; _rows[i].complete_popup_rate = int.TryParse( columns[9], out tmp9) ? tmp9 : 0;
				//Variable 'g_to_v_exchange' OF TYPE 'int'
				int tmp10; _rows[i].g_to_v_exchange = int.TryParse( columns[10], out tmp10) ? tmp10 : 0;
				//Variable 'freecash_count' OF TYPE 'int'
				int tmp11; _rows[i].freecash_count = int.TryParse( columns[11], out tmp11) ? tmp11 : 0;
				//Variable 'delivery_boost_ad' OF TYPE 'int'
				int tmp12; _rows[i].delivery_boost_ad = int.TryParse( columns[12], out tmp12) ? tmp12 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class RewardData{
		public string id;
		public enum eType{
			none,
			gold,
			cash,
			exp,
			random_box,
			truck_pc,
			parts,
			material,
			crate,
			booster,
			truck_id,
		}
		public eType type;
		public int _value;

		public RewardData(){}

		public RewardData(string id, RewardData.eType type, int _value){
			this.id = id;
			this.type = type;
			this._value = _value;
		}
	}
	public class SheetRewardData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,15);
		public readonly string[] labels = new string[]{"id","eNum type","value"};
		private RewardData[] _rows = new RewardData[11];
		public void Init() {
			_rows = new RewardData[]{
					new RewardData("none",RewardData.eType.none,0),
					new RewardData("gold",RewardData.eType.gold,1),
					new RewardData("cash",RewardData.eType.cash,2),
					new RewardData("exp",RewardData.eType.exp,3),
					new RewardData("random_box",RewardData.eType.random_box,4),
					new RewardData("truck_pc",RewardData.eType.truck_pc,5),
					new RewardData("parts",RewardData.eType.parts,6),
					new RewardData("material",RewardData.eType.material,7),
					new RewardData("crate",RewardData.eType.crate,8),
					new RewardData("booster",RewardData.eType.booster,9),
					new RewardData("truck_id",RewardData.eType.truck_id,100)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetRewardData t;
			public SheetEnumerator(SheetRewardData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public RewardData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public RewardData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public RewardData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public RewardData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public RewardData none{	get{ return _rows[0]; } }
		public RewardData gold{	get{ return _rows[1]; } }
		public RewardData cash{	get{ return _rows[2]; } }
		public RewardData exp{	get{ return _rows[3]; } }
		public RewardData random_box{	get{ return _rows[4]; } }
		public RewardData truck_pc{	get{ return _rows[5]; } }
		public RewardData parts{	get{ return _rows[6]; } }
		public RewardData material{	get{ return _rows[7]; } }
		public RewardData crate{	get{ return _rows[8]; } }
		public RewardData booster{	get{ return _rows[9]; } }
		public RewardData truck_id{	get{ return _rows[10]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 1, "rewardData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/o2uo282/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<RewardData> _rowsList = new List<RewardData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new RewardData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.RewardData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 3; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'type' OF TYPE 'RewardData.eType'
					_rows[i].type = (RewardData.eType)System.Enum.Parse( typeof(RewardData.eType), (columns[1] == "") ? "_default" : GetCamelCase(columns[1]));
				//Variable '_value' OF TYPE 'int'
				int tmp2; _rows[i]._value = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class FactorTypeData{
		public string id;
		public enum eType{
			none,
			reward_id,
			cargo_id,
			city_id,
			truck_id,
		}
		public eType type;
		public int _value;

		public FactorTypeData(){}

		public FactorTypeData(string id, FactorTypeData.eType type, int _value){
			this.id = id;
			this.type = type;
			this._value = _value;
		}
	}
	public class SheetFactorTypeData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,16);
		public readonly string[] labels = new string[]{"id","eNum type","value"};
		private FactorTypeData[] _rows = new FactorTypeData[5];
		public void Init() {
			_rows = new FactorTypeData[]{
					new FactorTypeData("none",FactorTypeData.eType.none,1),
					new FactorTypeData("reward_id",FactorTypeData.eType.reward_id,2),
					new FactorTypeData("cargo_id",FactorTypeData.eType.cargo_id,3),
					new FactorTypeData("city_id",FactorTypeData.eType.city_id,4),
					new FactorTypeData("truck_id",FactorTypeData.eType.truck_id,5)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetFactorTypeData t;
			public SheetEnumerator(SheetFactorTypeData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public FactorTypeData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public FactorTypeData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public FactorTypeData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public FactorTypeData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public FactorTypeData none{	get{ return _rows[0]; } }
		public FactorTypeData reward_id{	get{ return _rows[1]; } }
		public FactorTypeData cargo_id{	get{ return _rows[2]; } }
		public FactorTypeData city_id{	get{ return _rows[3]; } }
		public FactorTypeData truck_id{	get{ return _rows[4]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 2, "factorTypeData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/oxmbyps/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<FactorTypeData> _rowsList = new List<FactorTypeData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new FactorTypeData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.FactorTypeData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 3; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'type' OF TYPE 'FactorTypeData.eType'
					_rows[i].type = (FactorTypeData.eType)System.Enum.Parse( typeof(FactorTypeData.eType), (columns[1] == "") ? "_default" : GetCamelCase(columns[1]));
				//Variable '_value' OF TYPE 'int'
				int tmp2; _rows[i]._value = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class QuestData{
		public string id;
		public enum eType{
			mileage,
			cargo,
			cargo_id,
			arrive,
			reward_id,
			road,
			route,
			truck,
			map_object,
			truck_upgrade,
			city_upgrade,
			map_open,
			truck_repair,
			truck_refuel,
			truck_cash_refuel,
			truck_boost,
			watch_ad,
			random_box,
			to_city_reward_id,
			from_city_reward_id,
			to_city_cargo,
			from_city_cargo,
			to_city_cargo_id,
			from_city_cargo_id,
			cargo_to_city_id,
			cargo_from_city_id,
			delete_cargo,
			refresh_joblist,
			produce_parts,
			production_upgrade,
			delivery_start,
		}
		public eType type;
		public int quest_icon;
		public DatasTypes.FactorTypeData factor_type;
		public int count_mag_type;
		public int description;

		public QuestData(){}

		public QuestData(string id, QuestData.eType type, int quest_icon, DatasTypes.FactorTypeData factor_type, int count_mag_type, int description){
			this.id = id;
			this.type = type;
			this.quest_icon = quest_icon;
			this.factor_type = factor_type;
			this.count_mag_type = count_mag_type;
			this.description = description;
		}
	}
	public class SheetQuestData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,17);
		public readonly string[] labels = new string[]{"id","eNum type","quest_icon","factorTypeData factor_type","count_mag_type","description"};
		private QuestData[] _rows = new QuestData[31];
		public void Init() {
			_rows = new QuestData[]{
					new QuestData("mileage",QuestData.eType.mileage,2,Datas.factorTypeData.none,2,19001),
					new QuestData("cargo",QuestData.eType.cargo,2,Datas.factorTypeData.none,3,19002),
					new QuestData("cargo_id",QuestData.eType.cargo_id,4,Datas.factorTypeData.cargo_id,3,19003),
					new QuestData("arrive",QuestData.eType.arrive,2,Datas.factorTypeData.none,2,19004),
					new QuestData("reward_id",QuestData.eType.reward_id,3,Datas.factorTypeData.reward_id,4,19005),
					new QuestData("road",QuestData.eType.road,1,Datas.factorTypeData.none,1,19006),
					new QuestData("route",QuestData.eType.route,1,Datas.factorTypeData.none,1,19007),
					new QuestData("truck",QuestData.eType.truck,1,Datas.factorTypeData.none,1,19008),
					new QuestData("map_object",QuestData.eType.map_object,1,Datas.factorTypeData.none,1,19009),
					new QuestData("truck_upgrade",QuestData.eType.truck_upgrade,1,Datas.factorTypeData.none,1,19010),
					new QuestData("city_upgrade",QuestData.eType.city_upgrade,1,Datas.factorTypeData.none,1,19011),
					new QuestData("map_open",QuestData.eType.map_open,1,Datas.factorTypeData.none,1,19012),
					new QuestData("truck_repair",QuestData.eType.truck_repair,1,Datas.factorTypeData.none,1,19013),
					new QuestData("truck_refuel",QuestData.eType.truck_refuel,1,Datas.factorTypeData.none,2,19014),
					new QuestData("truck_cash_refuel",QuestData.eType.truck_cash_refuel,1,Datas.factorTypeData.none,2,19015),
					new QuestData("truck_boost",QuestData.eType.truck_boost,2,Datas.factorTypeData.none,2,19016),
					new QuestData("watch_ad",QuestData.eType.watch_ad,1,Datas.factorTypeData.none,1,19017),
					new QuestData("random_box",QuestData.eType.random_box,1,Datas.factorTypeData.none,1,19018),
					new QuestData("to_city_reward_id",QuestData.eType.to_city_reward_id,3,Datas.factorTypeData.reward_id,4,19019),
					new QuestData("from_city_reward_id",QuestData.eType.from_city_reward_id,3,Datas.factorTypeData.reward_id,4,19020),
					new QuestData("to_city_cargo",QuestData.eType.to_city_cargo,2,Datas.factorTypeData.none,3,19021),
					new QuestData("from_city_cargo",QuestData.eType.from_city_cargo,2,Datas.factorTypeData.none,3,19022),
					new QuestData("to_city_cargo_id",QuestData.eType.to_city_cargo_id,4,Datas.factorTypeData.cargo_id,3,19023),
					new QuestData("from_city_cargo_id",QuestData.eType.from_city_cargo_id,4,Datas.factorTypeData.cargo_id,3,19024),
					new QuestData("cargo_to_city_id",QuestData.eType.cargo_to_city_id,2,Datas.factorTypeData.city_id,3,19025),
					new QuestData("cargo_from_city_id",QuestData.eType.cargo_from_city_id,2,Datas.factorTypeData.city_id,3,19026),
					new QuestData("delete_cargo",QuestData.eType.delete_cargo,1,Datas.factorTypeData.none,1,19028),
					new QuestData("refresh_joblist",QuestData.eType.refresh_joblist,1,Datas.factorTypeData.none,1,19029),
					new QuestData("produce_parts",QuestData.eType.produce_parts,1,Datas.factorTypeData.none,1,19030),
					new QuestData("production_upgrade",QuestData.eType.production_upgrade,1,Datas.factorTypeData.none,1,19031),
					new QuestData("delivery_start",QuestData.eType.delivery_start,1,Datas.factorTypeData.none,1,19032)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetQuestData t;
			public SheetEnumerator(SheetQuestData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public QuestData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public QuestData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public QuestData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public QuestData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public QuestData mileage{	get{ return _rows[0]; } }
		public QuestData cargo{	get{ return _rows[1]; } }
		public QuestData cargo_id{	get{ return _rows[2]; } }
		public QuestData arrive{	get{ return _rows[3]; } }
		public QuestData reward_id{	get{ return _rows[4]; } }
		public QuestData road{	get{ return _rows[5]; } }
		public QuestData route{	get{ return _rows[6]; } }
		public QuestData truck{	get{ return _rows[7]; } }
		public QuestData map_object{	get{ return _rows[8]; } }
		public QuestData truck_upgrade{	get{ return _rows[9]; } }
		public QuestData city_upgrade{	get{ return _rows[10]; } }
		public QuestData map_open{	get{ return _rows[11]; } }
		public QuestData truck_repair{	get{ return _rows[12]; } }
		public QuestData truck_refuel{	get{ return _rows[13]; } }
		public QuestData truck_cash_refuel{	get{ return _rows[14]; } }
		public QuestData truck_boost{	get{ return _rows[15]; } }
		public QuestData watch_ad{	get{ return _rows[16]; } }
		public QuestData random_box{	get{ return _rows[17]; } }
		public QuestData to_city_reward_id{	get{ return _rows[18]; } }
		public QuestData from_city_reward_id{	get{ return _rows[19]; } }
		public QuestData to_city_cargo{	get{ return _rows[20]; } }
		public QuestData from_city_cargo{	get{ return _rows[21]; } }
		public QuestData to_city_cargo_id{	get{ return _rows[22]; } }
		public QuestData from_city_cargo_id{	get{ return _rows[23]; } }
		public QuestData cargo_to_city_id{	get{ return _rows[24]; } }
		public QuestData cargo_from_city_id{	get{ return _rows[25]; } }
		public QuestData delete_cargo{	get{ return _rows[26]; } }
		public QuestData refresh_joblist{	get{ return _rows[27]; } }
		public QuestData produce_parts{	get{ return _rows[28]; } }
		public QuestData production_upgrade{	get{ return _rows[29]; } }
		public QuestData delivery_start{	get{ return _rows[30]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 3, "questData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/oyskmzp/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<QuestData> _rowsList = new List<QuestData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new QuestData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.QuestData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 6; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'type' OF TYPE 'QuestData.eType'
					_rows[i].type = (QuestData.eType)System.Enum.Parse( typeof(QuestData.eType), (columns[1] == "") ? "_default" : GetCamelCase(columns[1]));
				//Variable 'quest_icon' OF TYPE 'int'
				int tmp2; _rows[i].quest_icon = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'factor_type' OF TYPE 'DatasTypes.FactorTypeData'
				_rows[i].factor_type = Datas.factorTypeData[ columns[3].Split('.').Last() ];
				//Variable 'count_mag_type' OF TYPE 'int'
				int tmp4; _rows[i].count_mag_type = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'description' OF TYPE 'int'
				int tmp5; _rows[i].description = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class GuideQuestData{
		public int index;
		public DatasTypes.QuestData qid;
		public int count;
		public int guide_id;
		public int string1;
		public int string2;
		public DatasTypes.RewardData reward_type;
		public string reward_index;
		public int reward_count;

		public GuideQuestData(){}

		public GuideQuestData(int index, DatasTypes.QuestData qid, int count, int guide_id, int string1, int string2, DatasTypes.RewardData reward_type, string reward_index, int reward_count){
			this.index = index;
			this.qid = qid;
			this.count = count;
			this.guide_id = guide_id;
			this.string1 = string1;
			this.string2 = string2;
			this.reward_type = reward_type;
			this.reward_index = reward_index;
			this.reward_count = reward_count;
		}
	}
	public class SheetGuideQuestData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,18);
		public readonly string[] labels = new string[]{"index","questData qid","count","guide_id","string1","string2","rewardData reward_type","string reward_index","int reward_count"};
		private GuideQuestData[] _rows = new GuideQuestData[7];
		public void Init() {
			_rows = new GuideQuestData[]{
					new GuideQuestData(1,Datas.questData.delivery_start,2,1,10102,10202,Datas.rewardData.truck_id,"0",1101),
					new GuideQuestData(2,Datas.questData.truck_boost,1,2,10101,10201,Datas.rewardData.gold,"0",3000),
					new GuideQuestData(3,Datas.questData.route,6,15,10103,10203,Datas.rewardData.crate,"crate1",10),
					new GuideQuestData(4,Datas.questData.road,9,16,10104,10204,Datas.rewardData.cash,"0",20),
					new GuideQuestData(5,Datas.questData.truck_upgrade,1,11,10105,10205,Datas.rewardData.gold,"0",5000),
					new GuideQuestData(6,Datas.questData.delete_cargo,1,4,10106,10206,Datas.rewardData.crate,"crate2",10),
					new GuideQuestData(7,Datas.questData.refresh_joblist,1,3,10107,10207,Datas.rewardData.cash,"0",30)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetGuideQuestData t;
			public SheetEnumerator(SheetGuideQuestData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public GuideQuestData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public GuideQuestData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public GuideQuestData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 4, "guideQuestData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/o7mfl6g/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<GuideQuestData> _rowsList = new List<GuideQuestData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new GuideQuestData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.GuideQuestData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 9; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'index' OF TYPE 'int'
				int tmp0; _rows[i].index = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'qid' OF TYPE 'DatasTypes.QuestData'
				_rows[i].qid = Datas.questData[ columns[1].Split('.').Last() ];
				//Variable 'count' OF TYPE 'int'
				int tmp2; _rows[i].count = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'guide_id' OF TYPE 'int'
				int tmp3; _rows[i].guide_id = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'string1' OF TYPE 'int'
				int tmp4; _rows[i].string1 = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'string2' OF TYPE 'int'
				int tmp5; _rows[i].string2 = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
				//Variable 'reward_type' OF TYPE 'DatasTypes.RewardData'
				_rows[i].reward_type = Datas.rewardData[ columns[6].Split('.').Last() ];
				//Variable 'reward_index' OF TYPE 'string'
				_rows[i].reward_index = (columns[7] == null) ? "" : columns[7].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'reward_count' OF TYPE 'int'
				int tmp8; _rows[i].reward_count = int.TryParse( columns[8], out tmp8) ? tmp8 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class AchievementData{
		public string name;
		public DatasTypes.QuestData[] qid;
		public int[] fid;
		public long[] count;
		public DatasTypes.RewardData[] reward_type;
		public string[] reward_index;
		public int[] reward_count;
		public int string_name;
		public int string_description;

		public AchievementData(){}

		public AchievementData(string name, DatasTypes.QuestData[] qid, int[] fid, long[] count, DatasTypes.RewardData[] reward_type, string[] reward_index, int[] reward_count, int string_name, int string_description){
			this.name = name;
			this.qid = qid;
			this.fid = fid;
			this.count = count;
			this.reward_type = reward_type;
			this.reward_index = reward_index;
			this.reward_count = reward_count;
			this.string_name = string_name;
			this.string_description = string_description;
		}
	}
	public class SheetAchievementData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,19);
		public readonly string[] labels = new string[]{"name","questData[] qid","int[] fid","long[] count","rewardData[] reward_type","string[] reward_index","int[] reward_count","string_name","string_description"};
		private AchievementData[] _rows = new AchievementData[15];
		public void Init() {
			_rows = new AchievementData[]{
					new AchievementData("Traveler",new DatasTypes.QuestData[]{ Datas.questData.mileage },new int[]{0},new long[]{5000,6700,9000,12100,16200,21700,29000,38800,51900,69400,92800,124000,165700,221400,295800,395200,528000,705400,942400,1259000,1682000,2247100,3002000,4010500,5357800,7157700,9562300,12774700,17066300,22799600},new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5000,6300,7800,9700,12100,15100,18800,23400,29100,36200,46000,57000,71000,88000,109000,136000,169000,211000,263000,327000,410000,510000,640000,790000,990000,1230000,1530000,1900000,2370000,2950000},19101,19201),
					new AchievementData("Transpoter",new DatasTypes.QuestData[]{ Datas.questData.cargo },new int[]{0},new long[]{100,140,200,280,400,570,810,1200,1700,2400,3400,4800,6800,9700,14000,20000,28000,40000,57000,81000,115000,160000,230000,330000,470000,670000,950000,1300000,1800000,2600000},new DatasTypes.RewardData[]{ Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate },new string[]{"crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2"},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},19102,19202),
					new AchievementData("Arrival",new DatasTypes.QuestData[]{ Datas.questData.arrive },new int[]{0},new long[]{30,50,70,100,140,190,260,350,470,630,850,1140,1530,2050,2740,3670,4910,6560,8770,11720,15660,20930,27970,37370,49930,66710,89130,119080,159090,212540},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10},19103,19203),
					new AchievementData("Get reach",new DatasTypes.QuestData[]{ Datas.questData.reward_id },new int[]{1},new long[]{30000,44000,64000,93000,140000,200000,290000,420000,610000,890000,1300000,1900000,2800000,4100000,6000000,8700000,13000000,19000000,28000000,41000000,60000000,87000000,130000000,190000000,280000000,410000000,600000000,870000000,1270000000,1800000000},new DatasTypes.RewardData[]{ Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate },new string[]{"crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2"},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},19104,19204),
					new AchievementData("Frontier",new DatasTypes.QuestData[]{ Datas.questData.road },new int[]{0},new long[]{10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64,66,68},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10},19105,19205),
					new AchievementData("Carrier",new DatasTypes.QuestData[]{ Datas.questData.route },new int[]{0},new long[]{7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10},19106,19206),
					new AchievementData("Collector",new DatasTypes.QuestData[]{ Datas.questData.truck },new int[]{0},new long[]{5,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},new DatasTypes.RewardData[]{ Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate },new string[]{"crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2"},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},19107,19207),
					new AchievementData("Pilot",new DatasTypes.QuestData[]{ Datas.questData.map_object },new int[]{0},new long[]{50,60,70,80,90,110,130,150,180,210,250,290,340,400,470,550,640,750,880,1030,1210,1420,1660,1950,2290,2680,3140,3680,4310,5050},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10},19108,19208),
					new AchievementData("Developer",new DatasTypes.QuestData[]{ Datas.questData.truck_upgrade },new int[]{0},new long[]{2,5,10,20,30,40,55,70,85,105,125,145,165,185,205,225,245,265,285,305,325},new DatasTypes.RewardData[]{ Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate },new string[]{"crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2"},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},19109,19209),
					new AchievementData("Mayor",new DatasTypes.QuestData[]{ Datas.questData.city_upgrade },new int[]{0},new long[]{2,6,12,20,30,42,56,72,90,110,132,156,182,210,240,275,315,365},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5,5,5,5,5,10,10,10,10,10,10,10,10,10,10,10,10,10},19110,19210),
					new AchievementData("Explorer",new DatasTypes.QuestData[]{ Datas.questData.map_open },new int[]{0},new long[]{1,1,1,1,1,1,1,1},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0"},new int[]{20,25,30,35,40,45,50,55},19111,19211),
					new AchievementData("Gas station",new DatasTypes.QuestData[]{ Datas.questData.truck_refuel },new int[]{0},new long[]{1000,1300,1700,2200,2900,3800,5000,6600,8700,11000,14000,18000,24000,32000,42000,55000,72000,95000,120000,160000,210000,280000,370000,490000,640000,840000,1100000,1400000,1800000,2400000},new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5000,6300,7800,9700,12100,15100,18800,23400,29100,36200,46000,57000,71000,88000,109000,136000,169000,211000,263000,327000,410000,510000,640000,790000,990000,1230000,1530000,1900000,2370000,2950000},19113,19213),
					new AchievementData("Teleporter",new DatasTypes.QuestData[]{ Datas.questData.truck_boost },new int[]{0},new long[]{5,5,5,10,10,10,15,15,15,20,20,20,30,30,30,40,40,40,50,50,50},new DatasTypes.RewardData[]{ Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5,5,5,5,5,5,5,5,5,10,10,10,10,10,10,10,10,10,10,10,10},19114,19214),
					new AchievementData("Audience",new DatasTypes.QuestData[]{ Datas.questData.watch_ad },new int[]{0},new long[]{20,30,40,50,60,70,80,90,95,100,105,110,115,120,125,130,135,140,145,150,155,160,165,170,175,180,185,190,195,200},new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold },new string[]{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"},new int[]{5000,6300,7800,9700,12100,15100,18800,23400,29100,36200,46000,57000,71000,88000,109000,136000,169000,211000,263000,327000,410000,510000,640000,790000,990000,1230000,1530000,1900000,2370000,2950000},19115,19215),
					new AchievementData("Keyman",new DatasTypes.QuestData[]{ Datas.questData.random_box },new int[]{0},new long[]{5,5,5,10,10,10,15,15,15,20,20,20,25,25,25,30,30,30,35,35,35,40,40,40,45,45,45,50,50,50},new DatasTypes.RewardData[]{ Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate },new string[]{"crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2","crate2"},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},19116,19216)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetAchievementData t;
			public SheetEnumerator(SheetAchievementData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public AchievementData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public AchievementData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].name == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].name == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public AchievementData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public AchievementData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public AchievementData traveler{	get{ return _rows[0]; } }
		public AchievementData transpoter{	get{ return _rows[1]; } }
		public AchievementData arrival{	get{ return _rows[2]; } }
		public AchievementData getReach{	get{ return _rows[3]; } }
		public AchievementData frontier{	get{ return _rows[4]; } }
		public AchievementData carrier{	get{ return _rows[5]; } }
		public AchievementData collector{	get{ return _rows[6]; } }
		public AchievementData pilot{	get{ return _rows[7]; } }
		public AchievementData developer{	get{ return _rows[8]; } }
		public AchievementData mayor{	get{ return _rows[9]; } }
		public AchievementData explorer{	get{ return _rows[10]; } }
		public AchievementData gasStation{	get{ return _rows[11]; } }
		public AchievementData teleporter{	get{ return _rows[12]; } }
		public AchievementData audience{	get{ return _rows[13]; } }
		public AchievementData keyman{	get{ return _rows[14]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 5, "achievementData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/obkzbdf/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<AchievementData> _rowsList = new List<AchievementData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new AchievementData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.AchievementData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 9; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'name' OF TYPE 'string'
				_rows[i].name = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'qid' OF TYPE 'DatasTypes.QuestData[]'
				List<QuestData> tmp1 = new List<QuestData>();
				foreach(string s in columns[1].Split(',')) {
					tmp1.Add( Datas.questData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].qid = tmp1.ToArray();
				//Variable 'fid' OF TYPE 'int[]'
				columns[2] = CleanString(columns[2]);	
				string[] tmp2 = columns[2].Split(',');
				_rows[i].fid =  (columns[2] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp2, ParseInt);
				//Variable 'count' OF TYPE 'long[]'
				columns[3] = CleanString(columns[3]);	
				string[] tmp3 = columns[3].Split(',');
				_rows[i].count =  (columns[3] == "") ? new long[]{} : Array.ConvertAll<string, long>(tmp3, ParseLong);
				//Variable 'reward_type' OF TYPE 'DatasTypes.RewardData[]'
				List<RewardData> tmp4 = new List<RewardData>();
				foreach(string s in columns[4].Split(',')) {
					tmp4.Add( Datas.rewardData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].reward_type = tmp4.ToArray();
				//Variable 'reward_index' OF TYPE 'string[]'
				columns[5] = CleanString(columns[5]);
				_rows[i].reward_index = (columns[5] == "") ? new string[]{} : columns[5].Split(',');
				//Variable 'reward_count' OF TYPE 'int[]'
				columns[6] = CleanString(columns[6]);	
				string[] tmp6 = columns[6].Split(',');
				_rows[i].reward_count =  (columns[6] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp6, ParseInt);
				//Variable 'string_name' OF TYPE 'int'
				int tmp7; _rows[i].string_name = int.TryParse( columns[7], out tmp7) ? tmp7 : 0;
				//Variable 'string_description' OF TYPE 'int'
				int tmp8; _rows[i].string_description = int.TryParse( columns[8], out tmp8) ? tmp8 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class LevelMission{
		public int lv;
		public int delay_time;
		public int[] reward_rate;
		public string[] reward_id;
		public int[] start_id;
		public string start_reward;

		public LevelMission(){}

		public LevelMission(int lv, int delay_time, int[] reward_rate, string[] reward_id, int[] start_id, string start_reward){
			this.lv = lv;
			this.delay_time = delay_time;
			this.reward_rate = reward_rate;
			this.reward_id = reward_id;
			this.start_id = start_id;
			this.start_reward = start_reward;
		}
	}
	public class SheetLevelMission: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,20);
		public readonly string[] labels = new string[]{"lv","delay_time","int[] reward_rate","string[] reward_id","int[] start_id","string start_reward"};
		private LevelMission[] _rows = new LevelMission[21];
		public void Init() {
			_rows = new LevelMission[]{
					new LevelMission(1,10,new int[]{75,25},new string[]{"random_box1","random_box2"},new int[]{1,2,8},"random_box1"),
					new LevelMission(6,15,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(9,20,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(12,25,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(16,30,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(21,35,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(26,40,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(31,45,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(36,50,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(41,55,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(46,60,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(51,65,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(56,70,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(61,75,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(66,80,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(71,85,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(76,90,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(81,95,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(86,100,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(91,110,new int[]{},new string[]{},new int[]{},""),
					new LevelMission(96,120,new int[]{},new string[]{},new int[]{},"")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetLevelMission t;
			public SheetEnumerator(SheetLevelMission t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public LevelMission this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public LevelMission[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public LevelMission Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 6, "levelMission", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/okeduvn/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<LevelMission> _rowsList = new List<LevelMission>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new LevelMission());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.LevelMission();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 6; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'lv' OF TYPE 'int'
				int tmp0; _rows[i].lv = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'delay_time' OF TYPE 'int'
				int tmp1; _rows[i].delay_time = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'reward_rate' OF TYPE 'int[]'
				columns[2] = CleanString(columns[2]);	
				string[] tmp2 = columns[2].Split(',');
				_rows[i].reward_rate =  (columns[2] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp2, ParseInt);
				//Variable 'reward_id' OF TYPE 'string[]'
				columns[3] = CleanString(columns[3]);
				_rows[i].reward_id = (columns[3] == "") ? new string[]{} : columns[3].Split(',');
				//Variable 'start_id' OF TYPE 'int[]'
				columns[4] = CleanString(columns[4]);	
				string[] tmp4 = columns[4].Split(',');
				_rows[i].start_id =  (columns[4] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp4, ParseInt);
				//Variable 'start_reward' OF TYPE 'string'
				_rows[i].start_reward = (columns[5] == null) ? "" : columns[5].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class LevelMissionData{
		public int id;
		public DatasTypes.QuestData[] qid;
		public int[] fid;
		public int[] count;
		public int rate;
		public int description;

		public LevelMissionData(){}

		public LevelMissionData(int id, DatasTypes.QuestData[] qid, int[] fid, int[] count, int rate, int description){
			this.id = id;
			this.qid = qid;
			this.fid = fid;
			this.count = count;
			this.rate = rate;
			this.description = description;
		}
	}
	public class SheetLevelMissionData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,21);
		public readonly string[] labels = new string[]{"id","questData[] qid","int[] fid","int[] count","rate","description"};
		private LevelMissionData[] _rows = new LevelMissionData[9];
		public void Init() {
			_rows = new LevelMissionData[]{
					new LevelMissionData(1,new DatasTypes.QuestData[]{ Datas.questData.mileage },new int[]{},new int[]{100},10,13001),
					new LevelMissionData(2,new DatasTypes.QuestData[]{ Datas.questData.cargo },new int[]{},new int[]{40},10,13002),
					new LevelMissionData(3,new DatasTypes.QuestData[]{ Datas.questData.arrive },new int[]{},new int[]{10},10,13003),
					new LevelMissionData(4,new DatasTypes.QuestData[]{ Datas.questData.reward_id },new int[]{1},new int[]{500},10,13004),
					new LevelMissionData(5,new DatasTypes.QuestData[]{ Datas.questData.reward_id },new int[]{2},new int[]{3},0,13005),
					new LevelMissionData(7,new DatasTypes.QuestData[]{ Datas.questData.reward_id },new int[]{3},new int[]{400},0,13007),
					new LevelMissionData(8,new DatasTypes.QuestData[]{ Datas.questData.map_object },new int[]{},new int[]{10},10,13008),
					new LevelMissionData(9,new DatasTypes.QuestData[]{ Datas.questData.cargo_to_city_id },new int[]{-1},new int[]{20},25,13009),
					new LevelMissionData(10,new DatasTypes.QuestData[]{ Datas.questData.cargo_from_city_id },new int[]{-1},new int[]{20},25,13010)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetLevelMissionData t;
			public SheetEnumerator(SheetLevelMissionData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public LevelMissionData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public LevelMissionData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public LevelMissionData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 7, "levelMissionData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/opl3bze/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<LevelMissionData> _rowsList = new List<LevelMissionData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new LevelMissionData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.LevelMissionData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 6; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'qid' OF TYPE 'DatasTypes.QuestData[]'
				List<QuestData> tmp1 = new List<QuestData>();
				foreach(string s in columns[1].Split(',')) {
					tmp1.Add( Datas.questData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].qid = tmp1.ToArray();
				//Variable 'fid' OF TYPE 'int[]'
				columns[2] = CleanString(columns[2]);	
				string[] tmp2 = columns[2].Split(',');
				_rows[i].fid =  (columns[2] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp2, ParseInt);
				//Variable 'count' OF TYPE 'int[]'
				columns[3] = CleanString(columns[3]);	
				string[] tmp3 = columns[3].Split(',');
				_rows[i].count =  (columns[3] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp3, ParseInt);
				//Variable 'rate' OF TYPE 'int'
				int tmp4; _rows[i].rate = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'description' OF TYPE 'int'
				int tmp5; _rows[i].description = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class NewDailyMission{
		public int id;
		public DatasTypes.QuestData qid;
		public int fid;
		public int count;
		public DatasTypes.RewardData reward_type;
		public string reward_id;
		public int cago_rate;

		public NewDailyMission(){}

		public NewDailyMission(int id, DatasTypes.QuestData qid, int fid, int count, DatasTypes.RewardData reward_type, string reward_id, int cago_rate){
			this.id = id;
			this.qid = qid;
			this.fid = fid;
			this.count = count;
			this.reward_type = reward_type;
			this.reward_id = reward_id;
			this.cago_rate = cago_rate;
		}
	}
	public class SheetNewDailyMission: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,21);
		public readonly string[] labels = new string[]{"id","questData qid","int fid","int count","rewardData reward_type","string reward_id","cago_rate"};
		private NewDailyMission[] _rows = new NewDailyMission[2];
		public void Init() {
			_rows = new NewDailyMission[]{
					new NewDailyMission(1,Datas.questData.to_city_cargo_id,-1,50,Datas.rewardData.crate,"crate2",15),
					new NewDailyMission(2,Datas.questData.from_city_cargo_id,-1,50,Datas.rewardData.crate,"crate2",60)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetNewDailyMission t;
			public SheetEnumerator(SheetNewDailyMission t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public NewDailyMission this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public NewDailyMission[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public NewDailyMission Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 8, "newDailyMission", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/oe1xegw/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<NewDailyMission> _rowsList = new List<NewDailyMission>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new NewDailyMission());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.NewDailyMission();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 7; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'qid' OF TYPE 'DatasTypes.QuestData'
				_rows[i].qid = Datas.questData[ columns[1].Split('.').Last() ];
				//Variable 'fid' OF TYPE 'int'
				int tmp2; _rows[i].fid = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'count' OF TYPE 'int'
				int tmp3; _rows[i].count = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'reward_type' OF TYPE 'DatasTypes.RewardData'
				_rows[i].reward_type = Datas.rewardData[ columns[4].Split('.').Last() ];
				//Variable 'reward_id' OF TYPE 'string'
				_rows[i].reward_id = (columns[5] == null) ? "" : columns[5].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'cago_rate' OF TYPE 'int'
				int tmp6; _rows[i].cago_rate = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class Days5EventData{
		public int step;
		public int[] city_id;
		public int delay_min;
		public DatasTypes.RewardData[] type;
		public string[] type_index;
		public int[] count;

		public Days5EventData(){}

		public Days5EventData(int step, int[] city_id, int delay_min, DatasTypes.RewardData[] type, string[] type_index, int[] count){
			this.step = step;
			this.city_id = city_id;
			this.delay_min = delay_min;
			this.type = type;
			this.type_index = type_index;
			this.count = count;
		}
	}
	public class SheetDays5EventData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,22);
		public readonly string[] labels = new string[]{"int step","int[] city_id","int delay_min","rewardData[] type","string[] type_index","int[] count"};
		private Days5EventData[] _rows = new Days5EventData[1];
		public void Init() {
			_rows = new Days5EventData[]{
					new Days5EventData(1,new int[]{303,406,506,602,701},1440,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.crate, Datas.rewardData.cash },new string[]{"0","crate2","0","crate2","0"},new int[]{2000,1,15,1,30})
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetDays5EventData t;
			public SheetEnumerator(SheetDays5EventData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public Days5EventData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public Days5EventData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public Days5EventData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 9, "days5EventData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/o7kc5lb/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<Days5EventData> _rowsList = new List<Days5EventData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new Days5EventData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.Days5EventData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 6; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'step' OF TYPE 'int'
				int tmp0; _rows[i].step = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'city_id' OF TYPE 'int[]'
				columns[1] = CleanString(columns[1]);	
				string[] tmp1 = columns[1].Split(',');
				_rows[i].city_id =  (columns[1] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp1, ParseInt);
				//Variable 'delay_min' OF TYPE 'int'
				int tmp2; _rows[i].delay_min = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'type' OF TYPE 'DatasTypes.RewardData[]'
				List<RewardData> tmp3 = new List<RewardData>();
				foreach(string s in columns[3].Split(',')) {
					tmp3.Add( Datas.rewardData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].type = tmp3.ToArray();
				//Variable 'type_index' OF TYPE 'string[]'
				columns[4] = CleanString(columns[4]);
				_rows[i].type_index = (columns[4] == "") ? new string[]{} : columns[4].Split(',');
				//Variable 'count' OF TYPE 'int[]'
				columns[5] = CleanString(columns[5]);	
				string[] tmp5 = columns[5].Split(',');
				_rows[i].count =  (columns[5] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp5, ParseInt);
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class LevelData{
		public int level;
		public long request_xp;
		public long reward_gold;
		public int reward_cash;
		public enum eUnlock{
			none,
			delivery,
			freeCash,
			operation,
			daily,
			level,
			review,
		}
		public eUnlock[] unlock;
		public int area_open;
		public int area_open_cost;
		public int[] pre_unlock;
		public int route_max;
		public int route_install_cost;
		public int route_delete_refund;
		public long route_price;

		public LevelData(){}

		public LevelData(int level, long request_xp, long reward_gold, int reward_cash, LevelData.eUnlock[] unlock, int area_open, int area_open_cost, int[] pre_unlock, int route_max, int route_install_cost, int route_delete_refund, long route_price){
			this.level = level;
			this.request_xp = request_xp;
			this.reward_gold = reward_gold;
			this.reward_cash = reward_cash;
			this.unlock = unlock;
			this.area_open = area_open;
			this.area_open_cost = area_open_cost;
			this.pre_unlock = pre_unlock;
			this.route_max = route_max;
			this.route_install_cost = route_install_cost;
			this.route_delete_refund = route_delete_refund;
			this.route_price = route_price;
		}
	}
	public class SheetLevelData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,23);
		public readonly string[] labels = new string[]{"level","long request_xp","long reward_gold","int reward_cash","eNum[] unlock","int area_open","int area_open_cost","int[] pre_unlock","int route_max","route_install_cost","route_delete_refund","long route_price"};
		private LevelData[] _rows = new LevelData[300];
		public void Init() {
			_rows = new LevelData[]{
					new LevelData(1,103,350,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},1,0,new int[]{0},40,1000,500,0),
					new LevelData(2,125,350,0,new LevelData.eUnlock[]{LevelData.eUnlock.delivery,LevelData.eUnlock.freeCash,LevelData.eUnlock.operation},2,50000,new int[]{0},0,0,0,3000),
					new LevelData(3,127,380,0,new LevelData.eUnlock[]{LevelData.eUnlock.daily},3,50000,new int[]{0},0,0,0,3000),
					new LevelData(4,150,390,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},4,50000,new int[]{3},0,0,0,3000),
					new LevelData(5,167,530,10,new LevelData.eUnlock[]{LevelData.eUnlock.level},5,50000,new int[]{4},0,0,0,3000),
					new LevelData(6,203,600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},6,50000,new int[]{5},0,0,0,3000),
					new LevelData(7,237,660,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},7,50000,new int[]{6},0,0,0,5000),
					new LevelData(8,275,720,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},8,50000,new int[]{7},0,0,0,8000),
					new LevelData(9,322,790,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,11000),
					new LevelData(10,366,950,10,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,15000),
					new LevelData(11,398,980,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,121000),
					new LevelData(12,440,1030,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,158000),
					new LevelData(13,476,1060,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,200000),
					new LevelData(14,514,1100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,256000),
					new LevelData(15,563,1150,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,318000),
					new LevelData(16,605,1190,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,665000),
					new LevelData(17,648,1230,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,821000),
					new LevelData(18,706,1290,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,1032000),
					new LevelData(19,753,1320,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,1254000),
					new LevelData(20,776,1630,10,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,1517000),
					new LevelData(21,832,1680,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,2196000),
					new LevelData(22,905,1750,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,2640000),
					new LevelData(23,967,1800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,3177000),
					new LevelData(24,1032,1850,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,3798000),
					new LevelData(25,1116,1950,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,4532000),
					new LevelData(26,1186,2000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,5394000),
					new LevelData(27,1259,2050,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,6350000),
					new LevelData(28,1355,2140,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,7468000),
					new LevelData(29,1437,2200,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,8771000),
					new LevelData(30,1522,2260,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,10289000),
					new LevelData(31,1638,2350,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,12067000),
					new LevelData(32,1741,2410,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,14140000),
					new LevelData(33,1849,2470,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,15300000),
					new LevelData(34,1988,2570,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,16556000),
					new LevelData(35,2112,2630,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,17906000),
					new LevelData(36,2240,2700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,19367000),
					new LevelData(37,2405,2800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,20949000),
					new LevelData(38,2550,2860,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,22646000),
					new LevelData(39,2705,2930,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,24478000),
					new LevelData(40,2869,3050,10,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,26448000),
					new LevelData(41,3075,3160,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(42,3257,3240,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(43,3456,3320,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(44,3705,3430,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(45,3929,3520,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(46,4166,3600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(47,4457,3720,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(48,4723,3810,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(49,5003,3900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(50,5351,4030,10,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,0),
					new LevelData(51,5586,4120,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(52,5824,4210,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(53,6134,4350,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(54,6399,4440,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(55,6681,4550,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(56,7037,4690,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(57,7344,4800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(58,7663,4910,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(59,7994,5020,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(60,8410,5270,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(61,8769,5390,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(62,9142,5510,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(63,9610,5670,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(64,10014,5790,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(65,10434,5910,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(66,10969,6090,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(67,11436,6220,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(68,11921,6360,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(69,12522,6540,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(70,13048,6680,10,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,0),
					new LevelData(71,13513,6820,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(72,14099,7010,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(73,14597,7160,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(74,15109,7300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(75,15753,7510,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(76,16315,7660,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(77,16895,7820,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(78,17504,7990,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(79,18250,8210,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(80,18888,8540,10,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,0),
					new LevelData(81,19545,8710,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(82,20362,8940,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(83,21079,9130,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(84,21801,9300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(85,22717,9560,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(86,23507,9750,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(87,24322,9940,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(88,25344,10210,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(89,26212,10410,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(90,27107,10610,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(91,28202,10890,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(92,29130,11100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(93,30083,11310,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(94,31304,11610,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(95,32344,11840,10,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(96,33436,12070,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(97,34536,12300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(98,35883,12610,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(99,37077,12850,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(100,38279,13290,20,new LevelData.eUnlock[]{LevelData.eUnlock.review},0,0,new int[]{},0,0,0,0),
					new LevelData(101,39805,13640,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(102,41112,13900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(103,42457,14160,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(104,44122,14510,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(105,45550,14780,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(106,47050,15070,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(107,48866,15400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(108,50461,15700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(109,52134,16000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(110,54117,16400,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(111,55893,16700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(112,57720,17000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(113,59882,17400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(114,61821,17800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(115,63813,18100,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(116,65903,18400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(117,68410,18900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(118,70584,19200,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(119,72864,19600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(120,75595,20600,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(121,77755,21000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(122,79915,21400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(123,82590,21900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(124,84913,22300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(125,87289,22700,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(126,90164,23200,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(127,92656,23700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(128,95262,24100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(129,98405,24600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(130,101144,25100,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(131,103945,25500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(132,107254,26100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(133,110253,26600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(134,113321,27100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(135,116459,27500,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(136,120225,28200,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(137,123520,28600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(138,126953,29200,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(139,131061,29800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(140,134665,30300,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(141,138352,30900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(142,142758,31600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(143,146697,32100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(144,150724,32700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(145,155525,33400,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(146,159751,34000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(147,164167,34600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(148,169394,35400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(149,174008,36000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(150,178724,36600,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(151,185117,37400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(152,191093,38100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(153,197122,38700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(154,203315,39400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(155,210551,40200,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(156,217109,40900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(157,224066,41600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(158,232032,42500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(159,239266,43200,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(160,246835,44000,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(161,255598,44900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(162,263572,45700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(163,271913,46500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(164,281439,47500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(165,290382,48300,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(166,299405,49100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(167,309876,50100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(168,319598,51000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(169,329526,51800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(170,341215,52900,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(171,351723,53800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(172,362841,54800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(173,374059,55700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(174,387241,56800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(175,399110,57800,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(176,411659,58800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(177,426127,60000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(178,439175,61000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(179,452963,62000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(180,468587,63300,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(181,483172,64400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(182,498151,65500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(183,515282,66800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(184,531292,67900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(185,547734,69000,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(186,566804,70500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(187,584075,71600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(188,602113,72800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(189,623002,74300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(190,642274,75500,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(191,662063,76800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(192,682233,78000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(193,705717,79600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(194,727405,80900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(195,749885,82300,20,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(196,775596,83900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(197,799363,85300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(198,823992,86700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(199,852126,88500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(200,878160,89900,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(201,896294,91400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(202,914692,92900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(203,933605,94400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(204,952793,96000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(205,972260,97500,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(206,992265,99100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(207,1012559,100800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(208,1033911,102500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(209,1055069,104100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(210,1076533,105800,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(211,1098574,107600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(212,1120932,109300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(213,1144436,111100,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(214,1167729,112900,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(215,1191355,114800,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(216,1215602,116600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(217,1240785,118600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(218,1266028,120500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(219,1291631,122400,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(220,1318513,124500,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(221,1345160,126500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(222,1372827,128500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(223,1400548,130600,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(224,1428663,132700,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(225,1458160,134900,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(226,1487400,137000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(227,1518068,139300,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(228,1548470,141500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(229,1580346,143800,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(230,1612686,146200,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(231,1645074,148500,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(232,1678678,151000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(233,1712329,153000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(234,1747238,156000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(235,1782994,158000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(236,1818449,161000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(237,1855577,164000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(238,1893239,166000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(239,1931797,169000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(240,1970034,171000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(241,2010052,174000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(242,2051011,177000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(243,2092555,180000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(244,2135066,183000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(245,2177232,186000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(246,2221329,189000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(247,2266052,192000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(248,2311799,195000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(249,2358588,198000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(250,2406040,201000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(251,2454565,204000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(252,2503775,208000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(253,2554088,211000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(254,2605524,214000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(255,2657684,218000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(256,2710999,221000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(257,2765489,225000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(258,2821928,228000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(259,2878410,232000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(260,2936122,236000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(261,2994640,239000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(262,3054422,243000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(263,3116779,247000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(264,3178719,251000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(265,3241981,255000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(266,3306587,259000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(267,3373469,263000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(268,3440375,267000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(269,3510114,272000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(270,3579878,276000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(271,3650606,280000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(272,3724303,285000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(273,3798017,289000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(274,3874800,294000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(275,3951093,298000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(276,4030548,303000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(277,4110012,308000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(278,4192746,313000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(279,4277173,318000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(280,4361074,323000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(281,4448400,328000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(282,4535722,333000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(283,4626585,338000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(284,4719275,344000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(285,4813821,349000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(286,4907792,355000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(287,5005533,360000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(288,5105212,366000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(289,5206858,372000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(290,5310505,377000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(291,5414124,383000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(292,5521833,389000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(293,5631639,395000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(294,5743575,402000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(295,5857676,408000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(296,5973352,414000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(297,6091248,421000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(298,6211401,427000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(299,6333845,434000,0,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0),
					new LevelData(300,6458616,441000,30,new LevelData.eUnlock[]{LevelData.eUnlock.none},0,0,new int[]{},0,0,0,0)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetLevelData t;
			public SheetEnumerator(SheetLevelData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public LevelData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public LevelData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public LevelData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 10, "levelData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/osvl9t5/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<LevelData> _rowsList = new List<LevelData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new LevelData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.LevelData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 12; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'level' OF TYPE 'int'
				int tmp0; _rows[i].level = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'request_xp' OF TYPE 'long'
				long tmp1; _rows[i].request_xp = long.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'reward_gold' OF TYPE 'long'
				long tmp2; _rows[i].reward_gold = long.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'reward_cash' OF TYPE 'int'
				int tmp3; _rows[i].reward_cash = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'unlock' OF TYPE 'LevelData.eUnlock[]'
				List<LevelData.eUnlock> tmp4 = new List<LevelData.eUnlock>();
				foreach(string val in columns[4].Split(',')) {
					if(val.Trim().Length > 0) {
						tmp4.Add((LevelData.eUnlock)System.Enum.Parse(typeof(LevelData.eUnlock), GetCamelCase(val)));
					}
				}
				_rows[i].unlock = tmp4.ToArray();		//prefix: 'LevelData.eUnlock' columnName: 'eNum[] unlock'
				//Variable 'area_open' OF TYPE 'int'
				int tmp5; _rows[i].area_open = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
				//Variable 'area_open_cost' OF TYPE 'int'
				int tmp6; _rows[i].area_open_cost = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
				//Variable 'pre_unlock' OF TYPE 'int[]'
				columns[7] = CleanString(columns[7]);	
				string[] tmp7 = columns[7].Split(',');
				_rows[i].pre_unlock =  (columns[7] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp7, ParseInt);
				//Variable 'route_max' OF TYPE 'int'
				int tmp8; _rows[i].route_max = int.TryParse( columns[8], out tmp8) ? tmp8 : 0;
				//Variable 'route_install_cost' OF TYPE 'int'
				int tmp9; _rows[i].route_install_cost = int.TryParse( columns[9], out tmp9) ? tmp9 : 0;
				//Variable 'route_delete_refund' OF TYPE 'int'
				int tmp10; _rows[i].route_delete_refund = int.TryParse( columns[10], out tmp10) ? tmp10 : 0;
				//Variable 'route_price' OF TYPE 'long'
				long tmp11; _rows[i].route_price = long.TryParse( columns[11], out tmp11) ? tmp11 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class CargoData{
		public int id;
		public string name;
		public float cargo_miles;
		public float cargo_route_bonus;
		public int cargo_randscale;
		public int cargo_on_route_rate;
		public int cargo_plus1_route_rate;
		public int cargo_remainder_route_rate;
		public int cargo_refresh;
		public int refresh_job_min;
		public int refresh_job_cash;

		public CargoData(){}

		public CargoData(int id, string name, float cargo_miles, float cargo_route_bonus, int cargo_randscale, int cargo_on_route_rate, int cargo_plus1_route_rate, int cargo_remainder_route_rate, int cargo_refresh, int refresh_job_min, int refresh_job_cash){
			this.id = id;
			this.name = name;
			this.cargo_miles = cargo_miles;
			this.cargo_route_bonus = cargo_route_bonus;
			this.cargo_randscale = cargo_randscale;
			this.cargo_on_route_rate = cargo_on_route_rate;
			this.cargo_plus1_route_rate = cargo_plus1_route_rate;
			this.cargo_remainder_route_rate = cargo_remainder_route_rate;
			this.cargo_refresh = cargo_refresh;
			this.refresh_job_min = refresh_job_min;
			this.refresh_job_cash = refresh_job_cash;
		}
	}
	public class SheetCargoData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,24);
		public readonly string[] labels = new string[]{"id","name","float cargo_miles","float cargo_route_bonus","cargo_randscale","int cargo_on_route_rate","int cargo_plus1_route_rate","int cargo_remainder_route_rate","int cargo_refresh","refresh_job_min","refresh_job_cash"};
		private CargoData[] _rows = new CargoData[50];
		public void Init() {
			_rows = new CargoData[]{
					new CargoData(1,"Firecracker",0.1f,100f,20,30,30,40,8,1,1),
					new CargoData(2,"Car",0f,0f,0,0,0,0,0,0,0),
					new CargoData(3,"Turntable",0f,0f,0,0,0,0,0,0,0),
					new CargoData(4,"Red carpet",0f,0f,0,0,0,0,0,0,0),
					new CargoData(5,"Chicken",0f,0f,0,0,0,0,0,0,0),
					new CargoData(6,"Bat and Ball",0f,0f,0,0,0,0,0,0,0),
					new CargoData(7,"Space food",0f,0f,0,0,0,0,0,0,0),
					new CargoData(8,"Basketball",0f,0f,0,0,0,0,0,0,0),
					new CargoData(9,"Fire ext.",0f,0f,0,0,0,0,0,0,0),
					new CargoData(10,"Guitar",0f,0f,0,0,0,0,0,0,0),
					new CargoData(11,"Mobile",0f,0f,0,0,0,0,0,0,0),
					new CargoData(12,"Snorkeling",0f,0f,0,0,0,0,0,0,0),
					new CargoData(13,"Hotdog",0f,0f,0,0,0,0,0,0,0),
					new CargoData(14,"Taco",0f,0f,0,0,0,0,0,0,0),
					new CargoData(15,"Teamwear",0f,0f,0,0,0,0,0,0,0),
					new CargoData(16,"Football",0f,0f,0,0,0,0,0,0,0),
					new CargoData(17,"Oil",0f,0f,0,0,0,0,0,0,0),
					new CargoData(18,"Stick and Puck",0f,0f,0,0,0,0,0,0,0),
					new CargoData(19,"Jack O",0f,0f,0,0,0,0,0,0,0),
					new CargoData(20,"Joypad",0f,0f,0,0,0,0,0,0,0),
					new CargoData(21,"Parka",0f,0f,0,0,0,0,0,0,0),
					new CargoData(22,"Handbell",0f,0f,0,0,0,0,0,0,0),
					new CargoData(23,"Flour",0f,0f,0,0,0,0,0,0,0),
					new CargoData(24,"Coffee",0f,0f,0,0,0,0,0,0,0),
					new CargoData(25,"Sugar",0f,0f,0,0,0,0,0,0,0),
					new CargoData(26,"Fabric",0f,0f,0,0,0,0,0,0,0),
					new CargoData(27,"Meat",0f,0f,0,0,0,0,0,0,0),
					new CargoData(28,"Concrete",0f,0f,0,0,0,0,0,0,0),
					new CargoData(29,"Tomato",0f,0f,0,0,0,0,0,0,0),
					new CargoData(30,"Apple",0f,0f,0,0,0,0,0,0,0),
					new CargoData(31,"Mineral",0f,0f,0,0,0,0,0,0,0),
					new CargoData(32,"Bread",0f,0f,0,0,0,0,0,0,0),
					new CargoData(33,"Iron",0f,0f,0,0,0,0,0,0,0),
					new CargoData(34,"Sand",0f,0f,0,0,0,0,0,0,0),
					new CargoData(35,"Milk",0f,0f,0,0,0,0,0,0,0),
					new CargoData(36,"Rope",0f,0f,0,0,0,0,0,0,0),
					new CargoData(37,"Resin",0f,0f,0,0,0,0,0,0,0),
					new CargoData(38,"Glass",0f,0f,0,0,0,0,0,0,0),
					new CargoData(39,"Silicon",0f,0f,0,0,0,0,0,0,0),
					new CargoData(40,"Fish",0f,0f,0,0,0,0,0,0,0),
					new CargoData(41,"Machine",0f,0f,0,0,0,0,0,0,0),
					new CargoData(42,"Wood",0f,0f,0,0,0,0,0,0,0),
					new CargoData(43,"Coal",0f,0f,0,0,0,0,0,0,0),
					new CargoData(44,"Stone",0f,0f,0,0,0,0,0,0,0),
					new CargoData(45,"Battery",0f,0f,0,0,0,0,0,0,0),
					new CargoData(46,"Cupcake",0f,0f,0,0,0,0,0,0,0),
					new CargoData(47,"Medicine",0f,0f,0,0,0,0,0,0,0),
					new CargoData(48,"Can",0f,0f,0,0,0,0,0,0,0),
					new CargoData(49,"Salt",0f,0f,0,0,0,0,0,0,0),
					new CargoData(50,"Papper",0f,0f,0,0,0,0,0,0,0)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCargoData t;
			public SheetEnumerator(SheetCargoData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public CargoData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public CargoData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public CargoData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 11, "cargoData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/okkssxb/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<CargoData> _rowsList = new List<CargoData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new CargoData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.CargoData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 11; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'name' OF TYPE 'string'
				_rows[i].name = (columns[1] == null) ? "" : columns[1].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'cargo_miles' OF TYPE 'float'
				float tmp2; _rows[i].cargo_miles = float.TryParse( columns[2], out tmp2) ? tmp2 : 0f;
				//Variable 'cargo_route_bonus' OF TYPE 'float'
				float tmp3; _rows[i].cargo_route_bonus = float.TryParse( columns[3], out tmp3) ? tmp3 : 0f;
				//Variable 'cargo_randscale' OF TYPE 'int'
				int tmp4; _rows[i].cargo_randscale = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'cargo_on_route_rate' OF TYPE 'int'
				int tmp5; _rows[i].cargo_on_route_rate = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
				//Variable 'cargo_plus1_route_rate' OF TYPE 'int'
				int tmp6; _rows[i].cargo_plus1_route_rate = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
				//Variable 'cargo_remainder_route_rate' OF TYPE 'int'
				int tmp7; _rows[i].cargo_remainder_route_rate = int.TryParse( columns[7], out tmp7) ? tmp7 : 0;
				//Variable 'cargo_refresh' OF TYPE 'int'
				int tmp8; _rows[i].cargo_refresh = int.TryParse( columns[8], out tmp8) ? tmp8 : 0;
				//Variable 'refresh_job_min' OF TYPE 'int'
				int tmp9; _rows[i].refresh_job_min = int.TryParse( columns[9], out tmp9) ? tmp9 : 0;
				//Variable 'refresh_job_cash' OF TYPE 'int'
				int tmp10; _rows[i].refresh_job_cash = int.TryParse( columns[10], out tmp10) ? tmp10 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class CargoLevelData{
		public int id;
		public int cargo_level;
		public int max_numb;
		public int gold_mag;
		public int xp_mag;
		public int cash_mag;
		public int user_lv;
		public int[] cargo_level_rate;

		public CargoLevelData(){}

		public CargoLevelData(int id, int cargo_level, int max_numb, int gold_mag, int xp_mag, int cash_mag, int user_lv, int[] cargo_level_rate){
			this.id = id;
			this.cargo_level = cargo_level;
			this.max_numb = max_numb;
			this.gold_mag = gold_mag;
			this.xp_mag = xp_mag;
			this.cash_mag = cash_mag;
			this.user_lv = user_lv;
			this.cargo_level_rate = cargo_level_rate;
		}
	}
	public class SheetCargoLevelData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,25);
		public readonly string[] labels = new string[]{"id","cargo_level","max_numb","gold_mag","xp_mag","cash_mag","user_lv","int[] cargo_level_rate"};
		private CargoLevelData[] _rows = new CargoLevelData[11];
		public void Init() {
			_rows = new CargoLevelData[]{
					new CargoLevelData(0,1,2,0,0,0,1,new int[]{70,20,10,0,0}),
					new CargoLevelData(1,2,3,100,0,0,3,new int[]{60,30,10,0,0}),
					new CargoLevelData(2,3,4,200,0,50,5,new int[]{40,40,15,5,0}),
					new CargoLevelData(3,4,5,300,0,100,10,new int[]{40,30,15,15,0}),
					new CargoLevelData(4,5,8,400,0,200,20,new int[]{30,30,15,15,10}),
					new CargoLevelData(5,0,0,0,0,0,25,new int[]{30,28,17,15,10}),
					new CargoLevelData(6,0,0,0,0,0,40,new int[]{30,26,17,17,10}),
					new CargoLevelData(7,0,0,0,0,0,60,new int[]{28,26,18,18,10}),
					new CargoLevelData(8,0,0,0,0,0,80,new int[]{30,30,20,15,5}),
					new CargoLevelData(9,0,0,0,0,0,100,new int[]{30,30,20,15,5}),
					new CargoLevelData(10,0,0,0,0,0,120,new int[]{30,25,20,15,10})
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCargoLevelData t;
			public SheetEnumerator(SheetCargoLevelData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public CargoLevelData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public CargoLevelData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public CargoLevelData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 12, "cargoLevelData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ocmvpfj/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<CargoLevelData> _rowsList = new List<CargoLevelData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new CargoLevelData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.CargoLevelData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 8; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'cargo_level' OF TYPE 'int'
				int tmp1; _rows[i].cargo_level = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'max_numb' OF TYPE 'int'
				int tmp2; _rows[i].max_numb = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'gold_mag' OF TYPE 'int'
				int tmp3; _rows[i].gold_mag = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'xp_mag' OF TYPE 'int'
				int tmp4; _rows[i].xp_mag = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'cash_mag' OF TYPE 'int'
				int tmp5; _rows[i].cash_mag = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
				//Variable 'user_lv' OF TYPE 'int'
				int tmp6; _rows[i].user_lv = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
				//Variable 'cargo_level_rate' OF TYPE 'int[]'
				columns[7] = CleanString(columns[7]);	
				string[] tmp7 = columns[7].Split(',');
				_rows[i].cargo_level_rate =  (columns[7] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp7, ParseInt);
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class CargoRewardData{
		public int index;
		public int gold;
		public int material;
		public int cash;
		public int crate;
		public int xp;
		public int rate;

		public CargoRewardData(){}

		public CargoRewardData(int index, int gold, int material, int cash, int crate, int xp, int rate){
			this.index = index;
			this.gold = gold;
			this.material = material;
			this.cash = cash;
			this.crate = crate;
			this.xp = xp;
			this.rate = rate;
		}
	}
	public class SheetCargoRewardData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,26);
		public readonly string[] labels = new string[]{"index","gold","material","cash","crate","xp","rate"};
		private CargoRewardData[] _rows = new CargoRewardData[15];
		public void Init() {
			_rows = new CargoRewardData[]{
					new CargoRewardData(1,25,1,0,0,10,3),
					new CargoRewardData(2,22,2,0,0,10,25),
					new CargoRewardData(3,20,3,0,0,10,30),
					new CargoRewardData(4,20,5,0,0,10,10),
					new CargoRewardData(5,15,7,0,0,10,5),
					new CargoRewardData(6,0,3,2,0,10,3),
					new CargoRewardData(7,0,3,3,0,10,3),
					new CargoRewardData(8,0,2,4,0,10,3),
					new CargoRewardData(9,0,2,5,0,10,3),
					new CargoRewardData(10,0,2,6,0,10,2),
					new CargoRewardData(11,0,1,7,0,10,2),
					new CargoRewardData(12,0,1,8,0,10,2),
					new CargoRewardData(13,0,1,9,0,10,1),
					new CargoRewardData(14,0,1,10,0,10,1),
					new CargoRewardData(15,0,3,0,1,10,7)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCargoRewardData t;
			public SheetEnumerator(SheetCargoRewardData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public CargoRewardData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public CargoRewardData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public CargoRewardData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 13, "cargoRewardData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ooj9ozm/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<CargoRewardData> _rowsList = new List<CargoRewardData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new CargoRewardData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.CargoRewardData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 7; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'index' OF TYPE 'int'
				int tmp0; _rows[i].index = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'gold' OF TYPE 'int'
				int tmp1; _rows[i].gold = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'material' OF TYPE 'int'
				int tmp2; _rows[i].material = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'cash' OF TYPE 'int'
				int tmp3; _rows[i].cash = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'crate' OF TYPE 'int'
				int tmp4; _rows[i].crate = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'xp' OF TYPE 'int'
				int tmp5; _rows[i].xp = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
				//Variable 'rate' OF TYPE 'int'
				int tmp6; _rows[i].rate = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class CityData{
		public int id;
		public string name;
		public bool mega;
		public int string_name;
		public string material_id;

		public CityData(){}

		public CityData(int id, string name, bool mega, int string_name, string material_id){
			this.id = id;
			this.name = name;
			this.mega = mega;
			this.string_name = string_name;
			this.material_id = material_id;
		}
	}
	public class SheetCityData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,27);
		public readonly string[] labels = new string[]{"id","name","bool mega","string_name","string material_id"};
		private CityData[] _rows = new CityData[56];
		public void Init() {
			_rows = new CityData[]{
					new CityData(101,"AUGUSTA",false,42101,""),
					new CityData(102,"MONTPELIER",false,42102,""),
					new CityData(103,"BUFFALO",false,42103,""),
					new CityData(104,"BOSTON",false,42104,""),
					new CityData(105,"PITTSBURGH",false,42105,""),
					new CityData(106,"NEW YORK",true,42106,"material1"),
					new CityData(107,"WASHINGTON D.C.",false,42107,""),
					new CityData(201,"MINNEAPOLIS",true,42201,"material2"),
					new CityData(202,"MILWAKEE",false,42202,""),
					new CityData(203,"DETROIT",false,42203,""),
					new CityData(204,"DAVENPORT",false,42204,""),
					new CityData(205,"CHICAGO",false,42205,""),
					new CityData(206,"INDIANAPOLIS",false,42206,""),
					new CityData(207,"CINCINNATI",false,42207,""),
					new CityData(301,"MEMPHIS",false,42301,""),
					new CityData(302,"BIRMINGHAM",false,42302,""),
					new CityData(303,"ATLANTA",true,42303,"material3"),
					new CityData(304,"CHARLOTTE",false,42304,""),
					new CityData(305,"SAVANNAH",false,42305,""),
					new CityData(306,"JACKSONVILLE",false,42306,""),
					new CityData(307,"MIAMI",false,42307,""),
					new CityData(401,"DES MOINES",false,42401,""),
					new CityData(402,"KANSAS CITY",false,42402,""),
					new CityData(403,"ST. LOUIS",false,42403,""),
					new CityData(404,"OKLAHOMA CITY",false,42404,""),
					new CityData(405,"LITTLE ROCK",false,42405,""),
					new CityData(406,"NEW ORLEANS",true,42406,"material4"),
					new CityData(407,"MONTGOMERY",false,42407,""),
					new CityData(501,"MILES CITY",false,42501,""),
					new CityData(502,"BISMARCK",false,42502,""),
					new CityData(503,"RAPID CITY",false,42503,""),
					new CityData(504,"CHEYENNE",false,42504,""),
					new CityData(505,"DENVER",true,42505,"material1"),
					new CityData(506,"LINCOLN",false,42506,""),
					new CityData(507,"WICHITA",false,42507,""),
					new CityData(601,"PHOENIX",false,42601,""),
					new CityData(602,"ALBUQUERQUE",false,42602,""),
					new CityData(603,"AMARILLO",false,42603,""),
					new CityData(604,"TUCSON",false,42604,""),
					new CityData(605,"EL PASO",false,42605,""),
					new CityData(606,"DALLAS",true,42606,"material2"),
					new CityData(607,"SAN ANTONIO",false,42607,""),
					new CityData(701,"SAN FRANCISCO",false,42701,""),
					new CityData(702,"RENO",false,42702,""),
					new CityData(703,"SALT LAKE CITY",false,42703,""),
					new CityData(704,"LOS ANGELES",false,42704,""),
					new CityData(705,"LAS VEGAS",true,42705,"material3"),
					new CityData(706,"GREEN RIVER",false,42706,""),
					new CityData(707,"SAN DIEGO",false,42707,""),
					new CityData(801,"SEATTLE",true,42801,"material4"),
					new CityData(802,"SPOKANE",false,42802,""),
					new CityData(803,"HELENA",false,42803,""),
					new CityData(804,"PORTLAND",false,42804,""),
					new CityData(805,"BOISE",false,42805,""),
					new CityData(806,"PLATORA",false,42806,""),
					new CityData(807,"TWIN FALLS",false,42807,"")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCityData t;
			public SheetEnumerator(SheetCityData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public CityData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public CityData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public CityData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 14, "cityData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ouahsoz/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<CityData> _rowsList = new List<CityData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new CityData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.CityData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 5; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'name' OF TYPE 'string'
				_rows[i].name = (columns[1] == null) ? "" : columns[1].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'mega' OF TYPE 'bool'
				bool tmp2;
				_rows[i].mega = ParseBool(columns[2]);
				//Variable 'string_name' OF TYPE 'int'
				int tmp3; _rows[i].string_name = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'material_id' OF TYPE 'string'
				_rows[i].material_id = (columns[4] == null) ? "" : columns[4].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class JoblistExpansion{
		public int max_numb;
		public int gold;
		public int cash;
		public int icon;
		public int city_img;

		public JoblistExpansion(){}

		public JoblistExpansion(int max_numb, int gold, int cash, int icon, int city_img){
			this.max_numb = max_numb;
			this.gold = gold;
			this.cash = cash;
			this.icon = icon;
			this.city_img = city_img;
		}
	}
	public class SheetJoblistExpansion: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,29);
		public readonly string[] labels = new string[]{"max_numb","gold","cash","icon","city_img"};
		private JoblistExpansion[] _rows = new JoblistExpansion[9];
		public void Init() {
			_rows = new JoblistExpansion[]{
					new JoblistExpansion(10,0,0,1,1),
					new JoblistExpansion(15,2000,50,2,2),
					new JoblistExpansion(20,5000,100,3,3),
					new JoblistExpansion(25,13000,200,4,4),
					new JoblistExpansion(30,41000,350,5,5),
					new JoblistExpansion(35,73000,500,6,6),
					new JoblistExpansion(40,127000,500,7,7),
					new JoblistExpansion(45,212000,500,8,7),
					new JoblistExpansion(50,402000,500,9,7)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetJoblistExpansion t;
			public SheetEnumerator(SheetJoblistExpansion t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public JoblistExpansion this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public JoblistExpansion[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public JoblistExpansion Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 15, "joblistExpansion", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ok6oe6z/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<JoblistExpansion> _rowsList = new List<JoblistExpansion>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new JoblistExpansion());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.JoblistExpansion();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 5; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'max_numb' OF TYPE 'int'
				int tmp0; _rows[i].max_numb = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'gold' OF TYPE 'int'
				int tmp1; _rows[i].gold = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'cash' OF TYPE 'int'
				int tmp2; _rows[i].cash = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'icon' OF TYPE 'int'
				int tmp3; _rows[i].icon = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'city_img' OF TYPE 'int'
				int tmp4; _rows[i].city_img = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class PartsProduction{
		public int id;
		public int city;
		public int max_level;
		public string output;
		public string input;
		public int[] parts_count;
		public int[] material_count;
		public int[] pd_gold;
		public int[] pd_time;
		public int[] up_gold;
		public int[] up_time;
		public float pd_boost_cash;
		public float up_boost_cash;

		public PartsProduction(){}

		public PartsProduction(int id, int city, int max_level, string output, string input, int[] parts_count, int[] material_count, int[] pd_gold, int[] pd_time, int[] up_gold, int[] up_time, float pd_boost_cash, float up_boost_cash){
			this.id = id;
			this.city = city;
			this.max_level = max_level;
			this.output = output;
			this.input = input;
			this.parts_count = parts_count;
			this.material_count = material_count;
			this.pd_gold = pd_gold;
			this.pd_time = pd_time;
			this.up_gold = up_gold;
			this.up_time = up_time;
			this.pd_boost_cash = pd_boost_cash;
			this.up_boost_cash = up_boost_cash;
		}
	}
	public class SheetPartsProduction: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,30);
		public readonly string[] labels = new string[]{"id","city","max_level","string output","string input","int[] parts_count","int[] material_count","int[] pd_gold","int[] pd_time","int[] up_gold","int[] up_time","float pd_boost_cash","float up_boost_cash"};
		private PartsProduction[] _rows = new PartsProduction[8];
		public void Init() {
			_rows = new PartsProduction[]{
					new PartsProduction(1,106,20,"parts1","material1",new int[]{10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20},new int[]{20,20,20,20,20,20,19,19,19,19,19,19,18,18,18,18,18,18,17,17},new int[]{1000,1260,1530,1790,2050,2320,2580,2840,3110,3370,3630,3890,4160,4420,4680,4950,5210,5470,5740,6000},new int[]{900,860,820,780,740,700,660,620,580,540,520,500,480,460,440,420,390,360,330,300},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,600,1800,3600,5400,7200,9000,10800,12600,14400,16200,18000,19800,21600,23400,25200,27000,28800,30600,32400},1.2f,1.275f),
					new PartsProduction(2,201,20,"parts2","material2",new int[]{10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20},new int[]{20,20,20,20,20,20,19,19,19,19,19,19,18,18,18,18,18,18,17,17},new int[]{1000,1260,1530,1790,2050,2320,2580,2840,3110,3370,3630,3890,4160,4420,4680,4950,5210,5470,5740,6000},new int[]{900,860,820,780,740,700,660,620,580,540,520,500,480,460,440,420,390,360,330,300},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,600,1800,3600,5400,7200,9000,10800,12600,14400,16200,18000,19800,21600,23400,25200,27000,28800,30600,32400},0f,0f),
					new PartsProduction(3,303,20,"parts3","material3",new int[]{10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20},new int[]{20,20,20,20,20,20,19,19,19,19,19,19,18,18,18,18,18,18,17,17},new int[]{1000,1260,1530,1790,2050,2320,2580,2840,3110,3370,3630,3890,4160,4420,4680,4950,5210,5470,5740,6000},new int[]{900,860,820,780,740,700,660,620,580,540,520,500,480,460,440,420,390,360,330,300},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,600,1800,3600,5400,7200,9000,10800,12600,14400,16200,18000,19800,21600,23400,25200,27000,28800,30600,32400},0f,0f),
					new PartsProduction(4,406,20,"parts4","material4",new int[]{10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20},new int[]{20,20,20,20,20,20,19,19,19,19,19,19,18,18,18,18,18,18,17,17},new int[]{1000,1260,1530,1790,2050,2320,2580,2840,3110,3370,3630,3890,4160,4420,4680,4950,5210,5470,5740,6000},new int[]{900,860,820,780,740,700,660,620,580,540,520,500,480,460,440,420,390,360,330,300},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,600,1800,3600,5400,7200,9000,10800,12600,14400,16200,18000,19800,21600,23400,25200,27000,28800,30600,32400},0f,0f),
					new PartsProduction(5,505,20,"parts1","material1",new int[]{50,54,57,60,63,65,67,70,72,75,77,80,82,85,87,90,92,95,97,100},new int[]{60,59,58,57,56,55,54,53,52,51,50,49,48,47,46,45,44,43,42,40},new int[]{10000,12600,15300,17900,20500,23200,25800,28400,31100,33700,36300,38900,41600,44200,46800,49500,52100,54700,57400,60000},new int[]{18000,17340,16740,16080,15480,14820,14220,13560,12960,12300,11700,11040,10440,9780,9180,8520,7920,7260,6660,6000},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,1200,3600,7200,10800,14400,18000,21600,25200,28800,32400,36000,39600,43200,46800,50400,54000,57600,61200,64800},0f,0f),
					new PartsProduction(6,606,20,"parts2","material2",new int[]{50,54,57,60,63,65,67,70,72,75,77,80,82,85,87,90,92,95,97,100},new int[]{60,59,58,57,56,55,54,53,52,51,50,49,48,47,46,45,44,43,42,40},new int[]{10000,12600,15300,17900,20500,23200,25800,28400,31100,33700,36300,38900,41600,44200,46800,49500,52100,54700,57400,60000},new int[]{18000,17340,16740,16080,15480,14820,14220,13560,12960,12300,11700,11040,10440,9780,9180,8520,7920,7260,6660,6000},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,1200,3600,7200,10800,14400,18000,21600,25200,28800,32400,36000,39600,43200,46800,50400,54000,57600,61200,64800},0f,0f),
					new PartsProduction(7,705,20,"parts3","material3",new int[]{50,54,57,60,63,65,67,70,72,75,77,80,82,85,87,90,92,95,97,100},new int[]{60,59,58,57,56,55,54,53,52,51,50,49,48,47,46,45,44,43,42,40},new int[]{10000,12600,15300,17900,20500,23200,25800,28400,31100,33700,36300,38900,41600,44200,46800,49500,52100,54700,57400,60000},new int[]{18000,17340,16740,16080,15480,14820,14220,13560,12960,12300,11700,11040,10440,9780,9180,8520,7920,7260,6660,6000},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,1200,3600,7200,10800,14400,18000,21600,25200,28800,32400,36000,39600,43200,46800,50400,54000,57600,61200,64800},0f,0f),
					new PartsProduction(8,801,20,"parts4","material4",new int[]{50,54,57,60,63,65,67,70,72,75,77,80,82,85,87,90,92,95,97,100},new int[]{60,59,58,57,56,55,54,53,52,51,50,49,48,47,46,45,44,43,42,40},new int[]{10000,12600,15300,17900,20500,23200,25800,28400,31100,33700,36300,38900,41600,44200,46800,49500,52100,54700,57400,60000},new int[]{18000,17340,16740,16080,15480,14820,14220,13560,12960,12300,11700,11040,10440,9780,9180,8520,7920,7260,6660,6000},new int[]{0,9000,19000,28000,40000,58000,78000,106000,145000,189000,251000,327000,416000,529000,679000,853000,1058000,1327000,1635000,2011000},new int[]{0,1200,3600,7200,10800,14400,18000,21600,25200,28800,32400,36000,39600,43200,46800,50400,54000,57600,61200,64800},0f,0f)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetPartsProduction t;
			public SheetEnumerator(SheetPartsProduction t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public PartsProduction this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public PartsProduction[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public PartsProduction Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 16, "partsProduction", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ovcz28d/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<PartsProduction> _rowsList = new List<PartsProduction>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new PartsProduction());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.PartsProduction();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 13; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'city' OF TYPE 'int'
				int tmp1; _rows[i].city = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'max_level' OF TYPE 'int'
				int tmp2; _rows[i].max_level = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'output' OF TYPE 'string'
				_rows[i].output = (columns[3] == null) ? "" : columns[3].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'input' OF TYPE 'string'
				_rows[i].input = (columns[4] == null) ? "" : columns[4].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'parts_count' OF TYPE 'int[]'
				columns[5] = CleanString(columns[5]);	
				string[] tmp5 = columns[5].Split(',');
				_rows[i].parts_count =  (columns[5] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp5, ParseInt);
				//Variable 'material_count' OF TYPE 'int[]'
				columns[6] = CleanString(columns[6]);	
				string[] tmp6 = columns[6].Split(',');
				_rows[i].material_count =  (columns[6] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp6, ParseInt);
				//Variable 'pd_gold' OF TYPE 'int[]'
				columns[7] = CleanString(columns[7]);	
				string[] tmp7 = columns[7].Split(',');
				_rows[i].pd_gold =  (columns[7] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp7, ParseInt);
				//Variable 'pd_time' OF TYPE 'int[]'
				columns[8] = CleanString(columns[8]);	
				string[] tmp8 = columns[8].Split(',');
				_rows[i].pd_time =  (columns[8] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp8, ParseInt);
				//Variable 'up_gold' OF TYPE 'int[]'
				columns[9] = CleanString(columns[9]);	
				string[] tmp9 = columns[9].Split(',');
				_rows[i].up_gold =  (columns[9] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp9, ParseInt);
				//Variable 'up_time' OF TYPE 'int[]'
				columns[10] = CleanString(columns[10]);	
				string[] tmp10 = columns[10].Split(',');
				_rows[i].up_time =  (columns[10] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp10, ParseInt);
				//Variable 'pd_boost_cash' OF TYPE 'float'
				float tmp11; _rows[i].pd_boost_cash = float.TryParse( columns[11], out tmp11) ? tmp11 : 0f;
				//Variable 'up_boost_cash' OF TYPE 'float'
				float tmp12; _rows[i].up_boost_cash = float.TryParse( columns[12], out tmp12) ? tmp12 : 0f;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class LevelMag{
		public int lv;
		public float count_1;
		public float count_2;
		public float count_3;
		public float count_4;
		public float reward_gold;
		public float reward_cash;
		public float reward_xp;
		public float reward_box;
		public float coinshop_mag;
		public int lucky_gold;
		public float count;
		public float reward;
		public float iAP_gold;

		public LevelMag(){}

		public LevelMag(int lv, float count_1, float count_2, float count_3, float count_4, float reward_gold, float reward_cash, float reward_xp, float reward_box, float coinshop_mag, int lucky_gold, float count, float reward, float iAP_gold){
			this.lv = lv;
			this.count_1 = count_1;
			this.count_2 = count_2;
			this.count_3 = count_3;
			this.count_4 = count_4;
			this.reward_gold = reward_gold;
			this.reward_cash = reward_cash;
			this.reward_xp = reward_xp;
			this.reward_box = reward_box;
			this.coinshop_mag = coinshop_mag;
			this.lucky_gold = lucky_gold;
			this.count = count;
			this.reward = reward;
			this.iAP_gold = iAP_gold;
		}
	}
	public class SheetLevelMag: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,31);
		public readonly string[] labels = new string[]{"lv","float count_1","float count_2","float count_3","float count_4","float reward_gold","float reward_cash","float reward_xp","float reward_box","float coinshop_mag","lucky_gold","float count","float reward","IAP_gold"};
		private LevelMag[] _rows = new LevelMag[300];
		public void Init() {
			_rows = new LevelMag[]{
					new LevelMag(1,1f,1f,1f,1f,1f,1f,1f,1f,1f,1,1f,1f,1f),
					new LevelMag(2,1f,1f,1.01f,1.01f,1.01f,1f,1.01f,1f,1f,1,1f,1f,1f),
					new LevelMag(3,1f,1f,1.02f,1.092857f,1.092857f,1f,1.02f,1f,1f,1,1f,1.153846f,1.01f),
					new LevelMag(4,1f,1f,1.03f,1.103571f,1.103571f,1f,1.03f,1f,1f,1,1f,1.173077f,1.01f),
					new LevelMag(5,1f,1.1f,1.144f,1.511714f,1.511714f,1f,1.144f,1f,1f,2,1f,1.423077f,1.05f),
					new LevelMag(6,1f,1.233333f,1.295f,1.71125f,1.71125f,1f,1.295f,1f,1f,2,1.6f,2.288461f,1.07f),
					new LevelMag(7,1f,1.333333f,1.413333f,1.867619f,1.867619f,1f,1.413333f,1f,1f,2,1.866667f,2.673077f,1.09f),
					new LevelMag(8,1f,1.433333f,1.533667f,2.026631f,2.026631f,1f,1.533667f,1f,2f,2,2.133333f,3.057692f,1.1f),
					new LevelMag(9,1f,1.566667f,1.692f,2.235857f,2.235857f,1f,1.692f,1f,2f,2,3f,4.307693f,1.12f),
					new LevelMag(10,1f,1.666667f,1.816667f,2.660119f,2.660119f,1f,1.816667f,1f,2f,3,3.333333f,5.230769f,1.17f),
					new LevelMag(11,1f,1.7f,1.87f,2.738214f,2.738214f,1f,1.87f,1f,2f,3,3.94152f,5.461538f,1.17f),
					new LevelMag(12,1f,1.766667f,1.964533f,2.876638f,2.876638f,1f,1.964533f,1f,2f,3,4.660674f,5.692307f,1.19f),
					new LevelMag(13,1f,1.8f,2.0232f,2.962543f,2.962543f,1f,2.0232f,1f,2f,3,5.511041f,5.942307f,1.2f),
					new LevelMag(14,1f,1.833333f,2.082667f,3.049619f,3.049619f,1f,2.082667f,1f,2f,3,6.516563f,6.173077f,1.2f),
					new LevelMag(15,1f,1.9f,2.1812f,3.1939f,3.1939f,1f,2.1812f,1f,2f,3,7.705549f,6.423077f,1.22f),
					new LevelMag(16,1f,1.933333f,2.242667f,3.283905f,3.283905f,1f,2.242667f,1f,2f,3,7.905211f,6.692307f,1.23f),
					new LevelMag(17,1f,1.966667f,2.304933f,3.375081f,3.375081f,1f,2.304933f,1f,2f,3,8.110048f,6.942307f,1.24f),
					new LevelMag(18,1f,2.033333f,2.407467f,3.525219f,3.525219f,1f,2.407467f,1f,2f,4,8.320192f,7.211538f,1.25f),
					new LevelMag(19,1f,2.066667f,2.471733f,3.619324f,3.619324f,1f,2.471733f,1f,2f,4,8.535782f,7.480769f,1.26f),
					new LevelMag(20,1f,2.1f,2.5368f,4.4394f,4.4394f,1f,2.5368f,1f,3f,4,8.756957f,8.019231f,1.34f),
					new LevelMag(21,1f,2.133333f,2.602667f,4.554667f,4.554667f,1f,2.602667f,1f,3f,5,8.983864f,8.307693f,1.36f),
					new LevelMag(22,1f,2.2f,2.7104f,4.7432f,4.7432f,1f,2.7104f,1f,3f,5,9.21665f,8.596154f,1.37f),
					new LevelMag(23,1f,2.233333f,2.778267f,4.861967f,4.861967f,1f,2.778267f,1f,3f,5,9.455468f,8.903846f,1.39f),
					new LevelMag(24,1f,2.266667f,2.846933f,4.982133f,4.982133f,1f,2.846933f,1f,3f,5,9.700474f,9.211538f,1.4f),
					new LevelMag(25,1f,2.333333f,2.958667f,5.219934f,5.219934f,1f,2.958667f,1f,3f,5,9.951828f,9.538462f,1.42f),
					new LevelMag(26,1f,2.366667f,3.029333f,5.34461f,5.34461f,1f,3.029333f,1f,3f,5,10.2097f,9.865385f,1.43f),
					new LevelMag(27,1f,2.4f,3.1008f,5.470697f,5.470697f,1f,3.1008f,1f,3f,5,10.47425f,10.19231f,1.45f),
					new LevelMag(28,1f,2.466667f,3.221467f,5.683588f,5.683588f,1f,3.221467f,1f,3f,6,10.74565f,10.51923f,1.47f),
					new LevelMag(29,1f,2.5f,3.3f,5.822143f,5.822143f,1f,3.3f,1f,3f,6,11.02409f,10.86539f,1.48f),
					new LevelMag(30,1f,2.533333f,3.379467f,5.962345f,5.962345f,1f,3.379467f,1f,3f,6,11.30974f,11.21154f,1.5f),
					new LevelMag(31,1f,2.6f,3.5048f,6.183468f,6.183468f,1f,3.5048f,1f,4f,6,11.5757f,11.57692f,1.52f),
					new LevelMag(32,1f,2.633333f,3.5866f,6.327787f,6.327787f,1f,3.5866f,1f,4f,6,11.84791f,11.94231f,1.53f),
					new LevelMag(33,1f,2.666667f,3.669333f,6.473752f,6.473752f,1f,3.669333f,1f,4f,6,12.12653f,12.30769f,1.55f),
					new LevelMag(34,1f,2.733333f,3.799333f,6.70311f,6.70311f,1f,3.799333f,1f,4f,7,12.4117f,12.69231f,1.57f),
					new LevelMag(35,1f,2.766667f,3.8844f,6.853191f,6.853191f,1f,3.8844f,1f,4f,7,12.70357f,13.07692f,1.59f),
					new LevelMag(36,1f,2.8f,3.9704f,7.00492f,7.00492f,1f,3.9704f,1f,4f,7,13.00231f,13.46154f,1.6f),
					new LevelMag(37,1f,2.866667f,4.105067f,7.24251f,7.24251f,1f,4.105067f,1f,4f,7,13.30808f,13.86539f,1.62f),
					new LevelMag(38,1f,2.9f,4.1934f,7.398355f,7.398355f,1f,4.1934f,1f,4f,7,13.62103f,14.26923f,1.64f),
					new LevelMag(39,1f,2.933333f,4.282667f,7.555848f,7.555848f,1f,4.282667f,1f,4f,8,13.94134f,14.69231f,1.66f),
					new LevelMag(40,1f,2.966667f,4.372867f,7.839925f,7.839925f,1f,4.372867f,1f,4f,8,14.26919f,15.11539f,1.68f),
					new LevelMag(41,1f,3.033333f,4.5136f,8.09224f,8.09224f,1f,4.5136f,1f,5f,8,14.60474f,15.53846f,1.71f),
					new LevelMag(42,1f,3.066667f,4.606133f,8.258139f,8.258139f,1f,4.606133f,1f,5f,8,14.94819f,15.98077f,1.73f),
					new LevelMag(43,1f,3.1f,4.7058f,8.436827f,8.436827f,1f,4.7058f,1f,5f,8,15.29971f,16.42308f,1.74f),
					new LevelMag(44,1f,3.166667f,4.857666f,8.709103f,8.709103f,1f,4.857666f,1f,5f,9,15.6595f,16.88461f,1.77f),
					new LevelMag(45,1f,3.2f,4.96f,8.892571f,8.892571f,1f,4.96f,1f,5f,9,16.02775f,17.34615f,1.79f),
					new LevelMag(46,1f,3.233333f,5.0634f,9.077952f,9.077952f,1f,5.0634f,1f,5f,9,16.37377f,17.82692f,1.81f),
					new LevelMag(47,1f,3.3f,5.2206f,9.35979f,9.35979f,1f,5.2206f,1f,5f,9,16.72725f,18.30769f,1.84f),
					new LevelMag(48,1f,3.333333f,5.326667f,9.549953f,9.549953f,1f,5.326667f,1f,5f,10,17.08837f,18.80769f,1.85f),
					new LevelMag(49,1f,3.366667f,5.4338f,9.742027f,9.742027f,1f,5.4338f,1f,5f,10,17.45728f,19.30769f,1.87f),
					new LevelMag(50,1f,3.433333f,5.596334f,10.03343f,10.03343f,1f,5.596334f,1f,6f,10,17.83416f,19.80769f,1.9f),
					new LevelMag(51,1f,3.466667f,5.706133f,10.23028f,10.23028f,1f,5.706133f,1f,6f,10,18.21917f,20.32692f,1.92f),
					new LevelMag(52,1f,3.5f,5.817f,10.42905f,10.42905f,1f,5.817f,1f,6f,10,18.6125f,20.86539f,1.94f),
					new LevelMag(53,1f,3.566667f,5.984867f,10.73001f,10.73001f,1f,5.984867f,1f,6f,11,19.01431f,21.40385f,1.97f),
					new LevelMag(54,1f,3.6f,6.0984f,10.93356f,10.93356f,1f,6.0984f,1f,6f,11,19.42481f,21.96154f,1.99f),
					new LevelMag(55,1f,3.633333f,6.220267f,11.15205f,11.15205f,1f,6.220267f,1f,6f,11,19.84416f,22.51923f,2.02f),
					new LevelMag(56,1f,3.7f,6.401f,11.47608f,11.47608f,1f,6.401f,1f,6f,11,20.27256f,23.07692f,2.05f),
					new LevelMag(57,1f,3.733333f,6.525867f,11.69995f,11.69995f,1f,6.525867f,1f,6f,12,20.71022f,23.65385f,2.07f),
					new LevelMag(58,1f,3.766667f,6.651933f,11.92597f,11.92597f,1f,6.651933f,1f,6f,12,21.15732f,24.25f,2.09f),
					new LevelMag(59,1f,3.8f,6.7792f,12.15414f,12.15414f,1f,6.7792f,1f,7f,12,21.61408f,24.84615f,2.12f),
					new LevelMag(60,1f,3.866667f,6.967733f,12.741f,12.741f,1f,6.967733f,1f,7f,13,22.08069f,25.46154f,2.17f),
					new LevelMag(61,1f,3.9f,7.098f,12.9792f,12.9792f,1f,7.098f,1f,7f,13,22.5114f,26.09615f,2.2f),
					new LevelMag(62,1f,3.933333f,7.229466f,13.2196f,13.2196f,1f,7.229466f,1f,7f,13,22.95051f,26.73077f,2.22f),
					new LevelMag(63,1f,4f,7.424f,13.57531f,13.57531f,1f,7.424f,1f,7f,14,23.39818f,27.36539f,2.26f),
					new LevelMag(64,1f,4.033333f,7.558466f,13.8212f,13.8212f,1f,7.558466f,1f,7f,14,23.85459f,28.03846f,2.28f),
					new LevelMag(65,1f,4.066667f,7.694133f,14.06927f,14.06927f,1f,7.694133f,1f,8f,14,24.31989f,28.69231f,2.31f),
					new LevelMag(66,1f,4.133333f,7.902933f,14.45108f,14.45108f,1f,7.902933f,1f,8f,14,24.79428f,29.38461f,2.35f),
					new LevelMag(67,1f,4.166667f,8.05f,14.72f,14.72f,1f,8.05f,1f,8f,15,25.27791f,30.07692f,2.37f),
					new LevelMag(68,1f,4.2f,8.1984f,14.99136f,14.99136f,1f,8.1984f,1f,8f,15,25.77098f,30.78846f,2.4f),
					new LevelMag(69,1f,4.266667f,8.413867f,15.38536f,15.38536f,1f,8.413867f,1f,8f,15,26.27367f,31.5f,2.44f),
					new LevelMag(70,1f,4.3f,8.5656f,15.66281f,15.66281f,1f,8.5656f,1f,8f,16,26.78617f,32.23077f,2.47f),
					new LevelMag(71,1f,4.333333f,8.718667f,15.94271f,15.94271f,1f,8.718667f,1f,8f,16,27.30866f,32.98077f,2.49f),
					new LevelMag(72,1f,4.4f,8.9408f,16.34889f,16.34889f,1f,8.9408f,1f,9f,16,27.84134f,33.73077f,2.53f),
					new LevelMag(73,1f,4.433333f,9.0972f,16.63488f,16.63488f,1f,9.0972f,1f,9f,17,28.38442f,34.5f,2.56f),
					new LevelMag(74,1f,4.466667f,9.254933f,16.92331f,16.92331f,1f,9.254933f,1f,9f,17,28.93808f,35.28846f,2.59f),
					new LevelMag(75,1f,4.533333f,9.483733f,17.34168f,17.34168f,1f,9.483733f,1f,9f,17,29.50255f,36.09615f,2.63f),
					new LevelMag(76,1f,4.566667f,9.653934f,17.65291f,17.65291f,1f,9.653934f,1f,9f,18,30.03992f,36.90385f,2.67f),
					new LevelMag(77,1f,4.6f,9.8256f,17.96681f,17.96681f,1f,9.8256f,1f,9f,18,30.58708f,37.73077f,2.7f),
					new LevelMag(78,1f,4.633333f,9.998734f,18.2834f,18.2834f,1f,9.998734f,1f,10f,18,31.14421f,38.57692f,2.73f),
					new LevelMag(79,1f,4.7f,10.246f,18.73554f,18.73554f,1f,10.246f,1f,10f,19,31.71148f,39.42308f,2.77f),
					new LevelMag(80,1f,4.733333f,10.4228f,19.43108f,19.43108f,1f,10.4228f,1f,10f,19,32.28909f,40.28846f,2.84f),
					new LevelMag(81,1f,4.766667f,10.60107f,19.76342f,19.76342f,1f,10.60107f,1f,10f,20,32.87721f,41.17308f,2.88f),
					new LevelMag(82,1f,4.833333f,10.85567f,20.23806f,20.23806f,1f,10.85567f,1f,11f,20,33.47605f,42.07692f,2.92f),
					new LevelMag(83,1f,4.866667f,11.0376f,20.57724f,20.57724f,1f,11.0376f,1f,11f,21,34.0858f,43f,2.96f),
					new LevelMag(84,1f,4.9f,11.221f,20.91915f,20.91915f,1f,11.221f,1f,11f,21,34.70665f,43.92308f,2.99f),
					new LevelMag(85,1f,4.966667f,11.49287f,21.42599f,21.42599f,1f,11.49287f,1f,11f,21,35.33881f,44.88462f,3.04f),
					new LevelMag(86,1f,5f,11.69f,21.7935f,21.7935f,1f,11.69f,1f,11f,22,35.98249f,45.84615f,3.08f),
					new LevelMag(87,1f,5.033333f,11.88873f,22.164f,22.164f,1f,11.88873f,1f,12f,22,36.63789f,46.82692f,3.12f),
					new LevelMag(88,1f,5.1f,12.1686f,22.68575f,22.68575f,1f,12.1686f,1f,12f,23,37.30523f,47.82692f,3.17f),
					new LevelMag(89,1f,5.133333f,12.37133f,23.0637f,23.0637f,1f,12.37133f,1f,12f,23,37.98472f,48.84615f,3.21f),
					new LevelMag(90,1f,5.166667f,12.57567f,23.44464f,23.44464f,1f,12.57567f,1f,12f,23,38.67659f,49.86538f,3.24f),
					new LevelMag(91,1f,5.233333f,12.86353f,23.9813f,23.9813f,1f,12.86353f,1f,12f,24,39.32299f,50.92308f,3.3f),
					new LevelMag(92,1f,5.266667f,13.07187f,24.36969f,24.36969f,1f,13.07187f,1f,13f,24,39.98019f,51.98077f,3.34f),
					new LevelMag(93,1f,5.3f,13.2818f,24.76107f,24.76107f,1f,13.2818f,1f,13f,25,40.64837f,53.07692f,3.38f),
					new LevelMag(94,1f,5.366667f,13.5884f,25.33266f,25.33266f,1f,13.5884f,1f,13f,25,41.32772f,54.17308f,3.43f),
					new LevelMag(95,1f,5.4f,13.8132f,25.75175f,25.75175f,1f,13.8132f,1f,13f,26,42.01843f,55.30769f,3.48f),
					new LevelMag(96,1f,5.433333f,14.03973f,26.17407f,26.17407f,1f,14.03973f,1f,14f,26,42.72068f,56.44231f,3.52f),
					new LevelMag(97,1f,5.466667f,14.268f,26.59963f,26.59963f,1f,14.268f,1f,14f,27,43.43466f,57.59615f,3.56f),
					new LevelMag(98,1f,5.533333f,14.58587f,27.19222f,27.19222f,1f,14.58587f,1f,14f,27,44.16058f,58.78846f,3.62f),
					new LevelMag(99,1f,5.566667f,14.81847f,27.62586f,27.62586f,1f,14.81847f,1f,14f,28,44.89863f,59.98077f,3.66f),
					new LevelMag(100,1f,5.6f,15.0528f,28.4928f,28.4928f,1f,15.0528f,1f,15f,28,45.64902f,61.21154f,3.75f),
					new LevelMag(101,1f,5.666667f,15.39067f,29.13233f,29.13233f,1f,15.39067f,1f,15f,29,46.41194f,62.44231f,3.81f),
					new LevelMag(102,1f,5.7f,15.6408f,29.6058f,29.6058f,1f,15.6408f,1f,15f,30,47.18762f,63.71154f,3.86f),
					new LevelMag(103,1f,5.733333f,15.8928f,30.0828f,30.0828f,1f,15.8928f,1f,16f,30,47.97626f,64.98077f,3.91f),
					new LevelMag(104,1f,5.8f,16.24f,30.74f,30.74f,1f,16.24f,1f,16f,31,48.77808f,66.28846f,3.97f),
					new LevelMag(105,1f,5.833333f,16.49667f,31.22583f,31.22583f,1f,16.49667f,1f,16f,31,49.5933f,67.61539f,4.02f),
					new LevelMag(106,1f,5.866667f,16.7552f,31.7152f,31.7152f,1f,16.7552f,1f,16f,32,50.41215f,68.96154f,4.07f),
					new LevelMag(107,1f,5.933333f,17.11173f,32.39007f,32.39007f,1f,17.11173f,1f,17f,32,51.24452f,70.32692f,4.14f),
					new LevelMag(108,1f,5.966667f,17.38687f,32.91085f,32.91085f,1f,17.38687f,1f,17f,33,52.09063f,71.73077f,4.19f),
					new LevelMag(109,1f,6f,17.664f,33.43543f,33.43543f,1f,17.664f,1f,17f,33,52.95071f,73.13461f,4.24f),
					new LevelMag(110,1f,6.066667f,18.04227f,34.15143f,34.15143f,1f,18.04227f,1f,18f,34,53.82499f,74.57692f,4.32f),
					new LevelMag(111,1f,6.1f,18.3244f,34.68547f,34.68547f,1f,18.3244f,1f,18f,35,54.71371f,76.03846f,4.37f),
					new LevelMag(112,1f,6.133333f,18.60853f,35.2233f,35.2233f,1f,18.60853f,1f,18f,35,55.6171f,77.51923f,4.42f),
					new LevelMag(113,1f,6.2f,18.9968f,35.95823f,35.95823f,1f,18.9968f,1f,18f,36,56.53541f,79.03846f,4.5f),
					new LevelMag(114,1f,6.233333f,19.28593f,36.50552f,36.50552f,1f,19.28593f,1f,19f,37,57.46888f,80.55769f,4.55f),
					new LevelMag(115,1f,6.266667f,19.5896f,37.08031f,37.08031f,1f,19.5896f,1f,19f,37,58.41776f,82.11539f,4.61f),
					new LevelMag(116,1f,6.3f,19.8954f,37.65915f,37.65915f,1f,19.8954f,1f,19f,38,59.38231f,83.71154f,4.67f),
					new LevelMag(117,1f,6.366667f,20.30967f,38.4433f,38.4433f,1f,20.30967f,1f,20f,38,60.36279f,85.32692f,4.74f),
					new LevelMag(118,1f,6.4f,20.6208f,39.03223f,39.03223f,1f,20.6208f,1f,20f,39,61.35946f,86.96154f,4.8f),
					new LevelMag(119,1f,6.433333f,20.93407f,39.6252f,39.6252f,1f,20.93407f,1f,20f,40,62.37257f,88.61539f,4.86f),
					new LevelMag(120,1f,6.5f,21.359f,41.65005f,41.65005f,1f,21.359f,1f,21f,42,63.40242f,90.30769f,5.07f),
					new LevelMag(121,1f,6.533333f,21.69067f,42.2968f,42.2968f,1f,21.69067f,1f,22f,42,64.37718f,92.01923f,5.13f),
					new LevelMag(122,1f,6.566667f,22.0246f,42.94797f,42.94797f,1f,22.0246f,1f,22f,43,65.36692f,93.76923f,5.19f),
					new LevelMag(123,1f,6.633333f,22.47373f,43.82378f,43.82378f,1f,22.47373f,1f,22f,44,66.37188f,95.53846f,5.28f),
					new LevelMag(124,1f,6.666667f,22.81333f,44.486f,44.486f,1f,22.81333f,1f,23f,44,67.39229f,97.34615f,5.35f),
					new LevelMag(125,1f,6.7f,23.1552f,45.15264f,45.15264f,1f,23.1552f,1f,23f,45,68.42839f,99.17308f,5.42f),
					new LevelMag(126,1f,6.766667f,23.61567f,46.05055f,46.05055f,1f,23.61567f,1f,24f,46,69.48042f,101.0385f,5.51f),
					new LevelMag(127,1f,6.8f,23.9768f,46.75476f,46.75476f,1f,23.9768f,1f,24f,47,70.54861f,102.9231f,5.58f),
					new LevelMag(128,1f,6.833333f,24.34033f,47.46365f,47.46365f,1f,24.34033f,1f,24f,47,71.63323f,104.8462f,5.65f),
					new LevelMag(129,1f,6.9f,24.8262f,48.41109f,48.41109f,1f,24.8262f,1f,25f,48,72.73454f,106.8077f,5.74f),
					new LevelMag(130,1f,6.933333f,25.19573f,49.13168f,49.13168f,1f,25.19573f,1f,25f,49,73.85276f,108.7885f,5.81f),
					new LevelMag(131,1f,6.966667f,25.56767f,49.85695f,49.85695f,1f,25.56767f,1f,25f,50,74.98818f,110.8077f,5.89f),
					new LevelMag(132,1f,7.033333f,26.06553f,50.82779f,50.82779f,1f,26.06553f,1f,26f,51,76.14106f,112.8462f,5.98f),
					new LevelMag(133,1f,7.066667f,26.4576f,51.59232f,51.59232f,1f,26.4576f,1f,26f,52,77.31166f,114.9231f,6.06f),
					new LevelMag(134,1f,7.1f,26.8522f,52.36179f,52.36179f,1f,26.8522f,1f,27f,52,78.50026f,117.0385f,6.14f),
					new LevelMag(135,1f,7.133333f,27.24933f,53.1362f,53.1362f,1f,27.24933f,1f,27f,53,79.70713f,119.1731f,6.21f),
					new LevelMag(136,1f,7.2f,27.7776f,54.16632f,54.16632f,1f,27.7776f,1f,28f,54,80.88544f,121.3654f,6.32f),
					new LevelMag(137,1f,7.233333f,28.18107f,54.95308f,54.95308f,1f,28.18107f,1f,28f,55,82.08118f,123.5769f,6.4f),
					new LevelMag(138,1f,7.266667f,28.6016f,55.77312f,55.77312f,1f,28.6016f,1f,28f,56,83.29459f,125.8269f,6.48f),
					new LevelMag(139,1f,7.333333f,29.15733f,56.8568f,56.8568f,1f,29.15733f,1f,29f,57,84.52592f,128.1154f,6.59f),
					new LevelMag(140,1f,7.366667f,29.58453f,57.68984f,57.68984f,1f,29.58453f,1f,29f,58,85.77547f,130.4231f,6.67f),
					new LevelMag(141,1f,7.4f,30.0144f,58.52808f,58.52808f,1f,30.0144f,1f,30f,59,87.0435f,132.7885f,6.75f),
					new LevelMag(142,1f,7.466667f,30.58347f,59.63776f,59.63776f,1f,30.58347f,1f,30f,60,88.33026f,135.1731f,6.86f),
					new LevelMag(143,1f,7.5f,31.035f,60.51825f,60.51825f,1f,31.035f,1f,31f,61,89.63605f,137.6154f,6.95f),
					new LevelMag(144,1f,7.533333f,31.48933f,61.4042f,61.4042f,1f,31.48933f,1f,31f,61,90.96114f,140.0769f,7.04f),
					new LevelMag(145,1f,7.6f,32.0872f,62.57004f,62.57004f,1f,32.0872f,1f,32f,63,92.30582f,142.5769f,7.16f),
					new LevelMag(146,1f,7.633333f,32.54853f,63.46964f,63.46964f,1f,32.54853f,1f,32f,63,93.67037f,145.1346f,7.25f),
					new LevelMag(147,1f,7.666667f,33.01266f,64.3747f,64.3747f,1f,33.01266f,1f,33f,64,95.05511f,147.7115f,7.34f),
					new LevelMag(148,1f,7.733333f,33.64f,65.598f,65.598f,1f,33.64f,1f,33f,66,96.4603f,150.3462f,7.46f),
					new LevelMag(149,1f,7.766667f,34.12673f,66.54713f,66.54713f,1f,34.12673f,1f,34f,67,97.88628f,153.0192f,7.55f),
					new LevelMag(150,1f,7.8f,34.6164f,67.50198f,67.50198f,1f,34.6164f,1f,34f,68,99.33334f,155.7308f,7.65f),
					new LevelMag(151,1f,7.866667f,35.2584f,68.75388f,68.75388f,1f,35.2584f,1f,35f,69,100.3239f,157.6346f,7.78f),
					new LevelMag(152,1f,7.9f,35.7712f,69.75384f,69.75384f,1f,35.7712f,1f,35f,70,101.3243f,159.5769f,7.88f),
					new LevelMag(153,1f,7.933333f,36.28707f,70.75978f,70.75978f,1f,36.28707f,1f,36f,71,102.3348f,161.5192f,7.98f),
					new LevelMag(154,1f,7.966667f,36.806f,71.7717f,71.7717f,1f,36.806f,1f,36f,72,103.3553f,163.5f,8.08f),
					new LevelMag(155,1f,8.033334f,37.48353f,73.09289f,73.09289f,1f,37.48353f,1f,37f,73,104.3859f,165.4808f,8.21f),
					new LevelMag(156,1f,8.066667f,38.01013f,74.11976f,74.11976f,1f,38.01013f,1f,38f,74,105.4269f,167.5f,8.31f),
					new LevelMag(157,1f,8.1f,38.556f,75.1842f,75.1842f,1f,38.556f,1f,38f,75,106.4782f,169.5192f,8.42f),
					new LevelMag(158,1f,8.166667f,39.26534f,76.5674f,76.5674f,1f,39.26534f,1f,39f,77,107.54f,171.5577f,8.56f),
					new LevelMag(159,1f,8.2f,39.8192f,77.64744f,77.64744f,1f,39.8192f,1f,39f,78,108.6124f,173.6154f,8.66f),
					new LevelMag(160,1f,8.233334f,40.37627f,78.73372f,78.73372f,1f,40.37627f,1f,40f,79,109.6955f,175.7115f,8.77f),
					new LevelMag(161,1f,8.3f,41.1182f,80.18049f,80.18049f,1f,41.1182f,1f,41f,80,110.7894f,177.8077f,8.92f),
					new LevelMag(162,1f,8.333333f,41.7f,81.315f,81.315f,1f,41.7f,1f,41f,81,111.8942f,179.9231f,9.03f),
					new LevelMag(163,1f,8.366667f,42.28513f,82.45601f,82.45601f,1f,42.28513f,1f,42f,82,113.01f,182.0577f,9.15f),
					new LevelMag(164,1f,8.433333f,43.04373f,83.93528f,83.93528f,1f,43.04373f,1f,42f,84,114.137f,184.2308f,9.29f),
					new LevelMag(165,1f,8.466666f,43.65413f,85.12556f,85.12556f,1f,43.65413f,1f,43f,85,115.2752f,186.4038f,9.41f),
					new LevelMag(166,1f,8.5f,44.268f,86.3226f,86.3226f,1f,44.268f,1f,44f,86,116.324f,188.5962f,9.53f),
					new LevelMag(167,1f,8.566667f,45.06067f,87.8683f,87.8683f,1f,45.06067f,1f,44f,88,117.3824f,190.8269f,9.69f),
					new LevelMag(168,1f,8.6f,45.6832f,89.08224f,89.08224f,1f,45.6832f,1f,45f,89,118.4504f,193.0577f,9.81f),
					new LevelMag(169,1f,8.633333f,46.32647f,90.33661f,90.33661f,1f,46.32647f,1f,46f,90,119.5281f,195.3077f,9.93f),
					new LevelMag(170,1f,8.7f,47.154f,91.9503f,91.9503f,1f,47.154f,1f,46f,92,120.6157f,197.5962f,10.1f),
					new LevelMag(171,1f,8.733334f,47.80627f,93.22222f,93.22222f,1f,47.80627f,1f,47f,93,121.7131f,199.8846f,10.22f),
					new LevelMag(172,1f,8.766666f,48.47967f,94.53535f,94.53535f,1f,48.47967f,1f,48f,95,122.8205f,202.2115f,10.35f),
					new LevelMag(173,1f,8.8f,49.1568f,95.85576f,95.85576f,1f,49.1568f,1f,48f,96,123.938f,204.5577f,10.49f),
					new LevelMag(174,1f,8.866667f,50.02573f,97.55018f,97.55018f,1f,50.02573f,1f,49f,98,125.0657f,206.9231f,10.66f),
					new LevelMag(175,1f,8.9f,50.7122f,98.88879f,98.88879f,1f,50.7122f,1f,50f,99,126.2036f,209.2885f,10.79f),
					new LevelMag(176,1f,8.933333f,51.42027f,100.2695f,100.2695f,1f,51.42027f,1f,51f,100,127.3519f,211.7115f,10.93f),
					new LevelMag(177,1f,9f,52.326f,102.0357f,102.0357f,1f,52.326f,1f,52f,102,128.5106f,214.1346f,11.1f),
					new LevelMag(178,1f,9.033334f,53.04373f,103.4353f,103.4353f,1f,53.04373f,1f,52f,103,129.6798f,216.5769f,11.24f),
					new LevelMag(179,1f,9.066667f,53.78347f,104.8778f,104.8778f,1f,53.78347f,1f,53f,105,130.8597f,219.0385f,11.39f),
					new LevelMag(180,1f,9.133333f,54.72693f,106.7175f,106.7175f,1f,54.72693f,1f,54f,107,132.0504f,221.5385f,11.57f),
					new LevelMag(181,1f,9.166667f,55.47667f,108.1795f,108.1795f,1f,55.47667f,1f,55f,108,133.2374f,224.0577f,11.72f),
					new LevelMag(182,1f,9.2f,56.2304f,109.6493f,109.6493f,1f,56.2304f,1f,55f,110,134.435f,226.5769f,11.86f),
					new LevelMag(183,1f,9.266666f,57.2124f,111.5642f,111.5642f,1f,57.2124f,1f,56f,112,135.6434f,229.1346f,12.06f),
					new LevelMag(184,1f,9.3f,57.9948f,113.0899f,113.0899f,1f,57.9948f,1f,57f,113,136.8627f,231.7308f,12.21f),
					new LevelMag(185,1f,9.333333f,58.78133f,114.6236f,114.6236f,1f,58.78133f,1f,58f,115,138.093f,234.3269f,12.36f),
					new LevelMag(186,1f,9.4f,59.8028f,116.6155f,116.6155f,1f,59.8028f,1f,59f,117,139.3343f,236.9615f,12.56f),
					new LevelMag(187,1f,9.433333f,60.6186f,118.2063f,118.2063f,1f,60.6186f,1f,60f,118,140.5867f,239.6154f,12.72f),
					new LevelMag(188,1f,9.466666f,61.43867f,119.8054f,119.8054f,1f,61.43867f,1f,60f,120,141.8505f,242.2885f,12.88f),
					new LevelMag(189,1f,9.533334f,62.50053f,121.876f,121.876f,1f,62.50053f,1f,61f,122,143.1255f,244.9808f,13.09f),
					new LevelMag(190,1f,9.566667f,63.35047f,123.5334f,123.5334f,1f,63.35047f,1f,62f,124,144.4121f,247.6923f,13.25f),
					new LevelMag(191,1f,9.6f,64.2048f,125.1994f,125.1994f,1f,64.2048f,1f,63f,125,145.7102f,250.4423f,13.42f),
					new LevelMag(192,1f,9.633333f,65.0828f,126.9115f,126.9115f,1f,65.0828f,1f,64f,127,147.02f,253.2115f,13.59f),
					new LevelMag(193,1f,9.7f,66.1928f,129.076f,129.076f,1f,66.1928f,1f,65f,129,148.3415f,256f,13.81f),
					new LevelMag(194,1f,9.733334f,67.08213f,130.8102f,130.8102f,1f,67.08213f,1f,66f,131,149.675f,258.8269f,13.98f),
					new LevelMag(195,1f,9.766666f,67.99554f,132.5913f,132.5913f,1f,67.99554f,1f,67f,133,151.0204f,261.6731f,14.16f),
					new LevelMag(196,1f,9.833333f,69.148f,134.8386f,134.8386f,1f,69.148f,1f,68f,135,151.6307f,264.5385f,14.38f),
					new LevelMag(197,1f,9.866667f,70.07307f,136.6425f,136.6425f,1f,70.07307f,1f,69f,137,152.2435f,267.4231f,14.56f),
					new LevelMag(198,1f,9.9f,71.0226f,138.4941f,138.4941f,1f,71.0226f,1f,70f,138,152.8587f,270.3462f,14.75f),
					new LevelMag(199,1f,9.966666f,72.21847f,140.826f,140.826f,1f,72.21847f,1f,71f,141,153.4765f,273.2885f,14.98f),
					new LevelMag(200,1f,10f,73.18f,142.701f,142.701f,1f,73.18f,1f,72f,143,154.0967f,276.25f,15.17f),
					new LevelMag(201,1f,10.03333f,74.1664f,144.6245f,144.6245f,1f,74.1664f,1f,73f,145,154.7195f,279.25f,15.36f),
					new LevelMag(202,1f,10.06667f,75.15773f,146.5576f,146.5576f,1f,75.15773f,1f,74f,147,155.3447f,282.2692f,15.56f),
					new LevelMag(203,1f,10.1f,76.1742f,148.5397f,148.5397f,1f,76.1742f,1f,75f,149,155.9725f,285.3077f,15.75f),
					new LevelMag(204,1f,10.13333f,77.19573f,150.5317f,150.5317f,1f,77.19573f,1f,76f,151,156.6028f,288.3846f,15.95f),
					new LevelMag(205,1f,10.16667f,78.22234f,152.5336f,152.5336f,1f,78.22234f,1f,77f,153,157.2357f,291.4808f,16.15f),
					new LevelMag(206,1f,10.2f,79.2744f,154.5851f,154.5851f,1f,79.2744f,1f,78f,155,157.8711f,294.6154f,16.36f),
					new LevelMag(207,1f,10.23333f,80.33167f,156.6467f,156.6467f,1f,80.33167f,1f,79f,157,158.5091f,297.7692f,16.56f),
					new LevelMag(208,1f,10.26667f,81.41467f,158.7586f,158.7586f,1f,81.41467f,1f,80f,159,159.1497f,300.9423f,16.78f),
					new LevelMag(209,1f,10.3f,82.503f,160.8808f,160.8808f,1f,82.503f,1f,81f,161,159.7929f,304.1538f,16.99f),
					new LevelMag(210,1f,10.33333f,83.59666f,163.0135f,163.0135f,1f,83.59666f,1f,82f,163,160.4386f,307.3846f,17.2f),
					new LevelMag(211,1f,10.36667f,84.7164f,165.197f,165.197f,1f,84.7164f,1f,83f,165,161.0105f,310.6538f,17.42f),
					new LevelMag(212,1f,10.4f,85.8416f,167.3911f,167.3911f,1f,85.8416f,1f,84f,167,161.5844f,313.9423f,17.64f),
					new LevelMag(213,1f,10.43333f,86.99313f,169.6366f,169.6366f,1f,86.99313f,1f,85f,170,162.1604f,317.2692f,17.86f),
					new LevelMag(214,1f,10.46667f,88.15027f,171.893f,171.893f,1f,88.15027f,1f,86f,172,162.7384f,320.6154f,18.09f),
					new LevelMag(215,1f,10.5f,89.313f,174.1604f,174.1604f,1f,89.313f,1f,88f,174,163.3185f,323.9808f,18.32f),
					new LevelMag(216,1f,10.53333f,90.5024f,176.4797f,176.4797f,1f,90.5024f,1f,89f,176,163.9006f,327.3846f,18.55f),
					new LevelMag(217,1f,10.56667f,91.69753f,178.8102f,178.8102f,1f,91.69753f,1f,90f,179,164.4849f,330.8269f,18.78f),
					new LevelMag(218,1f,10.6f,92.9196f,181.1932f,181.1932f,1f,92.9196f,1f,91f,181,165.0712f,334.2885f,19.02f),
					new LevelMag(219,1f,10.63333f,94.14753f,183.5877f,183.5877f,1f,94.14753f,1f,92f,184,165.6596f,337.7885f,19.26f),
					new LevelMag(220,1f,10.66667f,95.40266f,186.0352f,186.0352f,1f,95.40266f,1f,94f,186,166.25f,341.3077f,19.5f),
					new LevelMag(221,1f,10.7f,96.6638f,188.4944f,188.4944f,1f,96.6638f,1f,95f,188,166.8426f,344.8654f,19.75f),
					new LevelMag(222,1f,10.73333f,97.93093f,190.9653f,190.9653f,1f,97.93093f,1f,96f,191,167.4373f,348.4423f,20f),
					new LevelMag(223,1f,10.76667f,99.2256f,193.4899f,193.4899f,1f,99.2256f,1f,97f,193,168.0342f,352.0577f,20.25f),
					new LevelMag(224,1f,10.8f,100.5264f,196.0265f,196.0265f,1f,100.5264f,1f,99f,196,168.6331f,355.7115f,20.5f),
					new LevelMag(225,1f,10.83333f,101.855f,198.6172f,198.6172f,1f,101.855f,1f,100f,199,169.2342f,359.3846f,20.76f),
					new LevelMag(226,1f,10.86667f,103.1899f,201.2202f,201.2202f,1f,103.1899f,1f,101f,201,169.8592f,363.0769f,21.02f),
					new LevelMag(227,1f,10.9f,104.5528f,203.878f,203.878f,1f,104.5528f,1f,102f,204,170.4864f,366.8269f,21.29f),
					new LevelMag(228,1f,10.93333f,105.9221f,206.5482f,206.5482f,1f,105.9221f,1f,104f,207,171.116f,370.5962f,21.55f),
					new LevelMag(229,1f,10.96667f,107.3198f,209.2736f,209.2736f,1f,107.3198f,1f,105f,209,171.7479f,374.3846f,21.83f),
					new LevelMag(230,1f,11f,108.724f,212.0118f,212.0118f,1f,108.724f,1f,107f,212,172.3821f,378.2308f,22.1f),
					new LevelMag(231,1f,11.03333f,110.1568f,214.8058f,214.8058f,1f,110.1568f,1f,108f,215,173.0186f,382.0962f,22.38f),
					new LevelMag(232,1f,11.06667f,111.5963f,217.6127f,217.6127f,1f,111.5963f,1f,109f,218,173.6576f,385.9808f,22.66f),
					new LevelMag(233,1f,11.1f,113.0646f,220.476f,220.476f,1f,113.0646f,1f,111f,220,174.2988f,389.9231f,22.95f),
					new LevelMag(234,1f,11.13333f,114.5397f,223.3525f,223.3525f,1f,114.5397f,1f,112f,223,174.9425f,393.8846f,23.24f),
					new LevelMag(235,1f,11.16667f,116.044f,226.2858f,226.2858f,1f,116.044f,1f,114f,226,175.5885f,397.8846f,23.53f),
					new LevelMag(236,1f,11.2f,117.5552f,229.2326f,229.2326f,1f,117.5552f,1f,115f,229,176.2369f,401.9038f,23.82f),
					new LevelMag(237,1f,11.23333f,119.0958f,232.2368f,232.2368f,1f,119.0958f,1f,117f,232,176.8877f,405.9808f,24.12f),
					new LevelMag(238,1f,11.26667f,120.6435f,235.2548f,235.2548f,1f,120.6435f,1f,118f,235,177.5409f,410.0769f,24.43f),
					new LevelMag(239,1f,11.3f,122.2208f,238.3306f,238.3306f,1f,122.2208f,1f,120f,238,178.1965f,414.1923f,24.73f),
					new LevelMag(240,1f,11.33333f,123.8053f,241.4204f,241.4204f,1f,123.8053f,1f,121f,241,178.8546f,418.3654f,25.04f),
					new LevelMag(241,1f,11.36667f,125.4198f,244.5686f,244.5686f,1f,125.4198f,1f,123f,245,179.474f,422.5769f,25.36f),
					new LevelMag(242,1f,11.4f,127.0644f,247.7756f,247.7756f,1f,127.0644f,1f,124f,248,180.0956f,426.8077f,25.68f),
					new LevelMag(243,1f,11.43333f,128.7165f,250.9971f,250.9971f,1f,128.7165f,1f,126f,251,180.7193f,431.0769f,26f),
					new LevelMag(244,1f,11.46667f,130.3989f,254.2779f,254.2779f,1f,130.3989f,1f,128f,254,181.3452f,435.3846f,26.33f),
					new LevelMag(245,1f,11.5f,132.089f,257.5735f,257.5735f,1f,132.089f,1f,129f,258,181.9733f,439.7308f,26.66f),
					new LevelMag(246,1f,11.53333f,133.8097f,260.929f,260.929f,1f,133.8097f,1f,131f,261,182.6035f,444.0962f,26.99f),
					new LevelMag(247,1f,11.56667f,135.5382f,264.2995f,264.2995f,1f,135.5382f,1f,133f,264,183.236f,448.5192f,27.33f),
					new LevelMag(248,1f,11.6f,137.2976f,267.7303f,267.7303f,1f,137.2976f,1f,134f,268,183.8706f,452.9808f,27.67f),
					new LevelMag(249,1f,11.63333f,139.0881f,271.2219f,271.2219f,1f,139.0881f,1f,136f,271,184.5074f,457.4615f,28.02f),
					new LevelMag(250,1f,11.66667f,140.8867f,274.729f,274.729f,1f,140.8867f,1f,138f,275,185.1464f,461.9808f,28.37f),
					new LevelMag(251,1f,11.7f,142.7166f,278.2974f,278.2974f,1f,142.7166f,1f,140f,278,185.7876f,464.1346f,28.73f),
					new LevelMag(252,1f,11.73333f,144.5547f,281.8816f,281.8816f,1f,144.5547f,1f,141f,282,186.4311f,466.2692f,29.09f),
					new LevelMag(253,1f,11.76667f,146.4244f,285.5276f,285.5276f,1f,146.4244f,1f,143f,286,187.0768f,468.4423f,29.45f),
					new LevelMag(254,1f,11.8f,148.326f,289.2357f,289.2357f,1f,148.326f,1f,145f,289,187.7247f,470.5962f,29.82f),
					new LevelMag(255,1f,11.83333f,150.236f,292.9602f,292.9602f,1f,150.236f,1f,147f,293,188.3749f,472.7885f,30.2f),
					new LevelMag(256,1f,11.86667f,152.1781f,296.7473f,296.7473f,1f,152.1781f,1f,149f,297,188.9587f,474.9808f,30.57f),
					new LevelMag(257,1f,11.9f,154.1526f,300.5976f,300.5976f,1f,154.1526f,1f,151f,301,189.5443f,477.1731f,30.96f),
					new LevelMag(258,1f,11.93333f,156.1357f,304.4647f,304.4647f,1f,156.1357f,1f,153f,304,190.1318f,479.3846f,31.35f),
					new LevelMag(259,1f,11.96667f,158.1515f,308.3954f,308.3954f,1f,158.1515f,1f,155f,308,190.721f,481.5962f,31.74f),
					new LevelMag(260,1f,12f,160.2f,312.39f,312.39f,1f,160.2f,1f,157f,312,191.3121f,483.8269f,32.14f),
					new LevelMag(261,1f,12.03333f,162.2575f,316.4021f,316.4021f,1f,162.2575f,1f,159f,316,191.9051f,486.0769f,32.54f),
					new LevelMag(262,1f,12.06667f,164.348f,320.4786f,320.4786f,1f,164.348f,1f,161f,320,192.4998f,488.3269f,32.95f),
					new LevelMag(263,1f,12.1f,166.4718f,324.62f,324.62f,1f,166.4718f,1f,163f,325,193.0964f,490.5962f,33.36f),
					new LevelMag(264,1f,12.13333f,168.6048f,328.7794f,328.7794f,1f,168.6048f,1f,165f,329,193.6949f,492.8654f,33.78f),
					new LevelMag(265,1f,12.16667f,170.7713f,333.0041f,333.0041f,1f,170.7713f,1f,167f,333,194.2952f,495.1346f,34.2f),
					new LevelMag(266,1f,12.2f,172.9716f,337.2946f,337.2946f,1f,172.9716f,1f,169f,337,194.8974f,497.4423f,34.63f),
					new LevelMag(267,1f,12.23333f,175.1813f,341.6036f,341.6036f,1f,175.1813f,1f,171f,342,195.5014f,499.75f,35.06f),
					new LevelMag(268,1f,12.26667f,177.4251f,345.9789f,345.9789f,1f,177.4251f,1f,173f,346,196.1073f,502.0577f,35.5f),
					new LevelMag(269,1f,12.3f,179.703f,350.4208f,350.4208f,1f,179.703f,1f,176f,350,196.7151f,504.3846f,35.94f),
					new LevelMag(270,1f,12.33333f,182.0153f,354.9299f,354.9299f,1f,182.0153f,1f,178f,355,197.3248f,506.7115f,36.39f),
					new LevelMag(271,1f,12.36667f,184.3375f,359.4582f,359.4582f,1f,184.3375f,1f,180f,359,198.0014f,509.0577f,36.85f),
					new LevelMag(272,1f,12.4f,186.6944f,364.0541f,364.0541f,1f,186.6944f,1f,183f,364,198.6804f,511.4231f,37.31f),
					new LevelMag(273,1f,12.43333f,189.0861f,368.718f,368.718f,1f,189.0861f,1f,185f,369,199.3617f,513.7885f,37.77f),
					new LevelMag(274,1f,12.46667f,191.5129f,373.4502f,373.4502f,1f,191.5129f,1f,187f,373,200.0453f,516.1731f,38.25f),
					new LevelMag(275,1f,12.5f,193.95f,378.2025f,378.2025f,1f,193.95f,1f,190f,378,200.7313f,518.5577f,38.72f),
					new LevelMag(276,1f,12.53333f,196.4224f,383.0237f,383.0237f,1f,196.4224f,1f,192f,383,201.4196f,520.9615f,39.2f),
					new LevelMag(277,1f,12.56667f,198.9303f,387.9142f,387.9142f,1f,198.9303f,1f,194f,388,202.1103f,523.3846f,39.69f),
					new LevelMag(278,1f,12.6f,201.474f,392.8743f,392.8743f,1f,201.474f,1f,197f,393,202.8034f,525.8077f,40.19f),
					new LevelMag(279,1f,12.63333f,204.0536f,397.9045f,397.9045f,1f,204.0536f,1f,199f,398,203.4988f,528.2308f,40.69f),
					new LevelMag(280,1f,12.66667f,206.644f,402.9558f,402.9558f,1f,206.644f,1f,202f,403,204.1966f,530.6731f,41.2f),
					new LevelMag(281,1f,12.7f,209.2706f,408.0777f,408.0777f,1f,209.2706f,1f,205f,408,204.8968f,533.1346f,41.71f),
					new LevelMag(282,1f,12.73333f,211.9336f,413.2705f,413.2705f,1f,211.9336f,1f,207f,413,205.5994f,535.6154f,42.23f),
					new LevelMag(283,1f,12.76667f,214.6332f,418.5347f,418.5347f,1f,214.6332f,1f,210f,419,206.3044f,538.0961f,42.75f),
					new LevelMag(284,1f,12.8f,217.3696f,423.8707f,423.8707f,1f,217.3696f,1f,212f,424,207.0119f,540.5769f,43.29f),
					new LevelMag(285,1f,12.83333f,220.143f,429.2788f,429.2788f,1f,220.143f,1f,215f,429,207.7217f,543.0769f,43.83f),
					new LevelMag(286,1f,12.86667f,222.9279f,434.7094f,434.7094f,1f,222.9279f,1f,218f,435,208.3491f,545.5961f,44.37f),
					new LevelMag(287,1f,12.9f,225.75f,440.2125f,440.2125f,1f,225.75f,1f,221f,440,208.9783f,548.1346f,44.92f),
					new LevelMag(288,1f,12.93333f,228.6096f,445.7887f,445.7887f,1f,228.6096f,1f,223f,446,209.6094f,550.6731f,45.48f),
					new LevelMag(289,1f,12.96667f,231.5069f,451.4384f,451.4384f,1f,231.5069f,1f,226f,451,210.2425f,553.2115f,46.04f),
					new LevelMag(290,1f,13f,234.442f,457.1619f,457.1619f,1f,234.442f,1f,229f,457,210.8774f,555.7885f,46.62f),
					new LevelMag(291,1f,13.03333f,237.4152f,462.9596f,462.9596f,1f,237.4152f,1f,232f,463,211.5143f,558.3461f,47.2f),
					new LevelMag(292,1f,13.06667f,240.4267f,468.832f,468.832f,1f,240.4267f,1f,235f,469,212.1531f,560.9423f,47.78f),
					new LevelMag(293,1f,13.1f,243.4766f,474.7794f,474.7794f,1f,243.4766f,1f,238f,475,212.7938f,563.5385f,48.38f),
					new LevelMag(294,1f,13.13333f,246.5652f,480.8022f,480.8022f,1f,246.5652f,1f,241f,481,213.4364f,566.1539f,48.98f),
					new LevelMag(295,1f,13.16667f,249.6927f,486.9007f,486.9007f,1f,249.6927f,1f,244f,487,214.081f,568.7692f,49.59f),
					new LevelMag(296,1f,13.2f,252.8328f,493.024f,493.024f,1f,252.8328f,1f,247f,493,214.7276f,571.4039f,50.2f),
					new LevelMag(297,1f,13.23333f,256.0121f,499.2235f,499.2235f,1f,256.0121f,1f,250f,499,215.3761f,574.0577f,50.82f),
					new LevelMag(298,1f,13.26667f,259.2307f,505.4998f,505.4998f,1f,259.2307f,1f,253f,505,216.0265f,576.7115f,51.45f),
					new LevelMag(299,1f,13.3f,262.4888f,511.8531f,511.8531f,1f,262.4888f,1f,256f,512,216.6789f,579.3846f,52.09f),
					new LevelMag(300,1f,13.33333f,265.7867f,518.284f,518.284f,1f,265.7867f,1f,260f,518,217.3333f,582.0577f,52.73f)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetLevelMag t;
			public SheetEnumerator(SheetLevelMag t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public LevelMag this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public LevelMag[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public LevelMag Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 17, "levelMag", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/odnobuk/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<LevelMag> _rowsList = new List<LevelMag>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new LevelMag());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.LevelMag();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 14; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'lv' OF TYPE 'int'
				int tmp0; _rows[i].lv = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'count_1' OF TYPE 'float'
				float tmp1; _rows[i].count_1 = float.TryParse( columns[1], out tmp1) ? tmp1 : 0f;
				//Variable 'count_2' OF TYPE 'float'
				float tmp2; _rows[i].count_2 = float.TryParse( columns[2], out tmp2) ? tmp2 : 0f;
				//Variable 'count_3' OF TYPE 'float'
				float tmp3; _rows[i].count_3 = float.TryParse( columns[3], out tmp3) ? tmp3 : 0f;
				//Variable 'count_4' OF TYPE 'float'
				float tmp4; _rows[i].count_4 = float.TryParse( columns[4], out tmp4) ? tmp4 : 0f;
				//Variable 'reward_gold' OF TYPE 'float'
				float tmp5; _rows[i].reward_gold = float.TryParse( columns[5], out tmp5) ? tmp5 : 0f;
				//Variable 'reward_cash' OF TYPE 'float'
				float tmp6; _rows[i].reward_cash = float.TryParse( columns[6], out tmp6) ? tmp6 : 0f;
				//Variable 'reward_xp' OF TYPE 'float'
				float tmp7; _rows[i].reward_xp = float.TryParse( columns[7], out tmp7) ? tmp7 : 0f;
				//Variable 'reward_box' OF TYPE 'float'
				float tmp8; _rows[i].reward_box = float.TryParse( columns[8], out tmp8) ? tmp8 : 0f;
				//Variable 'coinshop_mag' OF TYPE 'float'
				float tmp9; _rows[i].coinshop_mag = float.TryParse( columns[9], out tmp9) ? tmp9 : 0f;
				//Variable 'lucky_gold' OF TYPE 'int'
				int tmp10; _rows[i].lucky_gold = int.TryParse( columns[10], out tmp10) ? tmp10 : 0;
				//Variable 'count' OF TYPE 'float'
				float tmp11; _rows[i].count = float.TryParse( columns[11], out tmp11) ? tmp11 : 0f;
				//Variable 'reward' OF TYPE 'float'
				float tmp12; _rows[i].reward = float.TryParse( columns[12], out tmp12) ? tmp12 : 0f;
				//Variable 'iAP_gold' OF TYPE 'float'
				float tmp13; _rows[i].iAP_gold = float.TryParse( columns[13], out tmp13) ? tmp13 : 0f;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class TruckData{
		public int id;
		public string model_h;
		public string model_c;
		public float offset_x;
		public float offset_y;
		public float offset_x2;
		public float offset_y2;
		public int cam_dis;
		public int cam_dis_big;
		public int max_lv;
		public int pieces;
		public int[] speed;
		public int[] fuel;
		public int[] cargo;
		public long[] gold;
		public long[] cash;
		public int[] parts1;
		public int[] parts2;
		public int[] parts3;
		public int[] parts4;
		public long[] resell_gold;
		public int name_id;

		public TruckData(){}

		public TruckData(int id, string model_h, string model_c, float offset_x, float offset_y, float offset_x2, float offset_y2, int cam_dis, int cam_dis_big, int max_lv, int pieces, int[] speed, int[] fuel, int[] cargo, long[] gold, long[] cash, int[] parts1, int[] parts2, int[] parts3, int[] parts4, long[] resell_gold, int name_id){
			this.id = id;
			this.model_h = model_h;
			this.model_c = model_c;
			this.offset_x = offset_x;
			this.offset_y = offset_y;
			this.offset_x2 = offset_x2;
			this.offset_y2 = offset_y2;
			this.cam_dis = cam_dis;
			this.cam_dis_big = cam_dis_big;
			this.max_lv = max_lv;
			this.pieces = pieces;
			this.speed = speed;
			this.fuel = fuel;
			this.cargo = cargo;
			this.gold = gold;
			this.cash = cash;
			this.parts1 = parts1;
			this.parts2 = parts2;
			this.parts3 = parts3;
			this.parts4 = parts4;
			this.resell_gold = resell_gold;
			this.name_id = name_id;
		}
	}
	public class SheetTruckData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,32);
		public readonly string[] labels = new string[]{"id","string model_h","string model_c","offset_x","offset_y","offset_x2","offset_y2","cam_dis","cam_dis_big","max_lv","pieces","int[] speed","int[] fuel","int[] cargo","long[] gold","long[] cash","int[] parts1","int[] parts2","int[] parts3","int[] parts4","long[] resell_gold","name_id"};
		private TruckData[] _rows = new TruckData[25];
		public void Init() {
			_rows = new TruckData[]{
					new TruckData(101,"head_A_1","cargo_00_A_1",-0.2f,-2.38f,-0.3f,-2f,-23,-16,5,3,new int[]{100,119,141,168,200,200,200},new int[]{376,447,531,631,750,750,750},new int[]{5,5,6,7,8,8,8},new long[]{4500,1125,2250,4500,9000,13500,18000},new long[]{0,1,1,2,3,5,6},new int[]{0,0,10,20,40,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new long[]{130,163,228,358,618,1008,1528},60001),
					new TruckData(201,"head_B_1","cargo_01_D_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,5,new int[]{83,99,118,140,167,167,167},new int[]{600,713,848,1009,1200,1200,1200},new int[]{6,7,8,9,11,11,11},new long[]{4500,1125,2250,4500,9000,13500,18000},new long[]{0,2,3,5,9,14,18},new int[]{0,0,20,40,60,0,0},new int[]{0,0,0,0,20,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new long[]{460,575,805,1265,2185,3565,5405},60002),
					new TruckData(202,"head_C_1","cargo_01_E_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,5,new int[]{62,74,88,105,125,125,125},new int[]{526,625,743,883,1050,1050,1050},new int[]{8,9,11,13,15,15,15},new long[]{4760,1190,2380,4760,9519,14278,19037},new long[]{0,2,4,7,14,21,28},new int[]{0,0,20,40,60,0,0},new int[]{0,0,0,0,20,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new long[]{690,863,1208,1898,3278,5348,8108},60003),
					new TruckData(301,"head_F_1","cargo_02_C_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{55,65,76,89,105,105,105},new int[]{674,802,954,1135,1350,1350,1350},new int[]{10,12,14,17,20,20,20},new long[]{4285,1072,2143,4285,8570,12855,17141},new long[]{0,3,5,9,18,27,36},new int[]{0,10,30,50,70,0,0},new int[]{0,0,0,30,50,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new long[]{864,1080,1512,2376,4104,6696,10152},60004),
					new TruckData(302,"head_D_1","cargo_02_D_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{47,54,63,73,85,85,85},new int[]{795,945,1124,1337,1590,1590,1590},new int[]{13,15,18,21,25,25,25},new long[]{4168,1042,2084,4168,8336,12504,16672},new long[]{0,3,6,11,21,32,42},new int[]{0,10,30,50,70,0,0},new int[]{0,0,0,30,50,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new long[]{1035,1294,1812,2847,4917,8022,12162},60005),
					new TruckData(303,"head_D_2","cargo_02_E_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{43,49,56,64,73,73,73},new int[]{824,980,1166,1387,1650,1650,1650},new int[]{15,18,21,25,30,30,30},new long[]{87736,21934,43868,87736,175472,263208,350944},new long[]{0,3,6,11,21,32,42},new int[]{0,10,30,50,70,0,0},new int[]{0,0,0,30,50,0,0},new int[]{0,0,0,0,0,0,0},new int[]{0,0,0,0,0,0,0},new long[]{1035,1294,1812,2847,4917,8022,12162},60006),
					new TruckData(401,"head_E_1","cargo_03_A_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{42,48,55,63,72,72,72},new int[]{1050,1249,1485,1766,2100,2100,2100},new int[]{17,20,24,29,34,34,34},new long[]{79185,19797,39593,79185,158370,237555,316740},new long[]{0,3,6,11,21,32,42},new int[]{0,30,50,50,70,0,0},new int[]{0,0,0,60,100,0,0},new int[]{0,0,0,50,80,0,0},new int[]{0,0,0,0,0,0,0},new long[]{1084,1355,1897,2981,5149,8401,12736},60101),
					new TruckData(402,"head_E_2","cargo_03_B_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{42,47,53,60,68,68,68},new int[]{1005,1195,1421,1690,2010,2010,2010},new int[]{17,21,25,30,36,36,36},new long[]{79395,19849,39698,79395,158790,238185,317580},new long[]{0,3,5,10,19,29,38},new int[]{0,30,50,50,70,0,0},new int[]{0,0,0,60,100,0,0},new int[]{0,0,0,50,80,0,0},new int[]{0,0,0,0,0,0,0},new long[]{989,1236,1731,2720,4698,7665,11621},60102),
					new TruckData(501,"head_G_1","cargo_04_A_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{40,43,47,51,56,56,56},new int[]{1065,1266,1506,1791,2130,2130,2130},new int[]{21,25,31,38,46,46,46},new long[]{79395,19849,39698,79395,158790,238185,317580},new long[]{0,3,5,10,19,29,38},new int[]{0,50,70,100,0,0,0},new int[]{0,0,30,50,100,0,0},new int[]{0,0,30,70,100,0,0},new int[]{0,0,0,0,80,0,0},new long[]{963,1204,1685,2648,4573,7461,11311},60103),
					new TruckData(502,"head_H_1","cargo_04_E_1",-0.34f,-2.7f,-0.3f,-2f,-26,-16,5,10,new int[]{40,43,46,49,52,52,52},new int[]{1125,1338,1591,1892,2250,2250,2250},new int[]{21,26,32,40,50,50,50},new long[]{79395,21155,42309,84618,169236,253854,338472},new long[]{0,5,10,19,38,57,76},new int[]{0,50,70,100,0,0,0},new int[]{0,0,30,50,100,0,0},new int[]{0,0,30,70,100,0,0},new int[]{0,0,0,0,80,0,0},new long[]{1005,1256,1758,2763,4773,7787,11806},60104),
					new TruckData(1101,"head_SP1_1","0",-0.8f,-2.7f,-0.3f,-2f,-29,-19,5,9,new int[]{70,87,108,134,167,167,167},new int[]{1050,1249,1485,1766,2100,2100,2100},new int[]{15,18,21,25,30,30,30},new long[]{288356,72089,144178,288356,576712,865067,1153423},new long[]{22,3,6,11,22,33,44},new int[]{0,20,30,0,0,0,0},new int[]{0,20,30,50,70,0,0},new int[]{0,0,20,40,60,0,0},new int[]{0,0,0,30,50,0,0},new long[]{2234,2513,3072,4189,6424,9776,14245},60105),
					new TruckData(1102,"head_SP1_2","0",-0.8f,-2.7f,-0.3f,-2f,-29,-19,5,9,new int[]{67,84,104,129,160,160,160},new int[]{975,1160,1379,1640,1950,1950,1950},new int[]{15,18,22,26,31,31,31},new long[]{261783,65446,130892,261783,523565,785347,1047130},new long[]{0,7,13,26,51,77,102},new int[]{0,20,30,0,0,0,0},new int[]{0,20,30,50,70,0,0},new int[]{0,0,20,40,60,0,0},new int[]{0,0,0,30,50,0,0},new long[]{5670,7088,9923,15593,26934,43945,66627},60201),
					new TruckData(1103,"head_SP1_3","0",-0.8f,-2.7f,-0.3f,-2f,-29,-19,5,9,new int[]{63,79,98,122,152,152,152},new int[]{900,1070,1273,1514,1800,1800,1800},new int[]{17,20,24,29,34,34,34},new long[]{262079,65520,131040,262079,524157,786235,1048313},new long[]{0,6,12,24,47,71,94},new int[]{0,20,30,0,0,0,0},new int[]{0,20,30,50,70,0,0},new int[]{0,0,20,40,60,0,0},new int[]{0,0,0,30,50,0,0},new long[]{5226,6532,9145,14371,24822,40499,61401},60202),
					new TruckData(1201,"head_FireEngine","cargo_FireEngine",-0.34f,-2.2f,-0.3f,-1.8f,-23,-14,5,12,new int[]{70,87,108,134,167,167,167},new int[]{1110,1320,1570,1867,2220,2220,2220},new int[]{17,20,24,29,35,35,35},new long[]{262079,65520,131040,262079,524157,786235,1048313},new long[]{0,7,13,25,49,74,98},new int[]{0,30,40,0,0,0,0},new int[]{0,30,40,60,80,0,0},new int[]{0,0,30,50,80,0,0},new int[]{0,0,0,50,80,0,0},new long[]{5466,6833,9566,15032,25964,42362,64226},60203),
					new TruckData(1202,"head_Halloween","cargo_Halloween",-0.8f,-3.4f,-0.3f,-3f,-29,-20,5,12,new int[]{64,80,99,123,153,153,153},new int[]{1080,1284,1527,1816,2160,2160,2160},new int[]{19,23,27,32,38,38,38},new long[]{262079,71793,143585,287170,574339,861508,1148678},new long[]{0,11,22,44,88,132,176},new int[]{0,30,40,0,0,0,0},new int[]{0,30,40,60,80,0,0},new int[]{0,0,30,50,80,0,0},new int[]{0,0,0,50,80,0,0},new long[]{4956,6195,8673,13629,23541,38409,58234},60204),
					new TruckData(1203,"head_Hotdog","cargo_Hotdog",-0.34f,-2.7f,-0.3f,-2f,-26,-17,5,12,new int[]{64,76,90,107,127,127,127},new int[]{1096,1303,1549,1842,2190,2190,2190},new int[]{21,25,30,37,45,45,45},new long[]{668951,167238,334476,668951,1337902,2006853,2675804},new long[]{52,7,13,26,52,78,104},new int[]{0,30,40,0,0,0,0},new int[]{0,30,40,60,80,0,0},new int[]{0,0,30,50,80,0,0},new int[]{0,0,0,50,80,0,0},new long[]{11616,13068,15972,21780,33395,50818,74048},60205),
					new TruckData(1204,"head_SP2_1","cargo_00_SP2_1",-2f,-2.7f,-1f,-2f,-28,-17,5,12,new int[]{60,71,85,101,120,120,120},new int[]{1200,1427,1697,2018,2400,2400,2400},new int[]{23,28,34,41,50,50,50},new long[]{601742,150436,300871,601742,1203484,1805227,2406969},new long[]{0,10,20,40,80,120,160},new int[]{0,30,40,0,0,0,0},new int[]{0,30,40,60,80,0,0},new int[]{0,0,30,50,80,0,0},new int[]{0,0,0,50,80,0,0},new long[]{10980,13725,19215,30195,52156,85097,129018},60301),
					new TruckData(1301,"head_SP3_1","0",-0.34f,-2.7f,0.4f,-2f,-28,-17,5,15,new int[]{64,76,90,107,127,127,127},new int[]{1215,1445,1718,2043,2430,2430,2430},new int[]{27,33,41,50,62,62,62},new long[]{603335,150834,301668,603335,1206670,1810005,2413340},new long[]{0,10,19,38,75,113,150},new int[]{0,0,0,0,0,0,0},new int[]{0,60,80,100,120,0,0},new int[]{0,20,40,70,100,0,0},new int[]{0,50,70,100,150,0,0},new long[]{10315,12894,18051,28366,48995,79939,121197},60302),
					new TruckData(1302,"head_SP3_2","0",-0.34f,-2.7f,0.4f,-2f,-28,-17,5,15,new int[]{61,73,87,103,123,123,123},new int[]{1291,1535,1825,2170,2580,2580,2580},new int[]{28,35,43,53,65,65,65},new long[]{603335,150834,301668,603335,1206670,1810005,2413340},new long[]{0,10,19,37,74,111,148},new int[]{0,0,0,0,0,0,0},new int[]{0,60,80,100,120,0,0},new int[]{0,20,40,70,100,0,0},new int[]{0,50,70,100,150,0,0},new long[]{10156,12695,17773,27929,48241,78709,119333},60303),
					new TruckData(1401,"head_I_1","cargo_04_D_1",-1.5f,-2.7f,-0.3f,-2f,-27,-16,5,18,new int[]{66,79,94,112,133,133,133},new int[]{1335,1588,1888,2245,2670,2670,2670},new int[]{32,40,49,60,74,74,74},new long[]{603335,162707,325413,650826,1301652,1952477,2603303},new long[]{0,19,37,73,146,219,292},new int[]{0,60,0,150,0,0,0},new int[]{0,70,100,0,200,0,0},new int[]{0,0,90,120,150,0,0},new int[]{0,80,120,160,200,0,0},new long[]{10090,12613,17658,27748,47928,78198,118558},60304),
					new TruckData(1402,"head_J_1","cargo_04_A_2",-1.5f,-2.2f,-0.3f,-2f,-27,-16,5,18,new int[]{63,75,89,106,126,126,126},new int[]{1350,1605,1909,2270,2700,2700,2700},new int[]{35,43,53,65,80,80,80},new long[]{1240677,310170,620339,1240677,2481354,3722031,4962709},new long[]{81,11,21,41,81,122,162},new int[]{0,60,0,150,0,0,0},new int[]{0,70,100,0,200,0,0},new int[]{0,0,90,120,150,0,0},new int[]{0,80,120,160,200,0,0},new long[]{22406,25207,30808,42011,64416,98024,142834},60305),
					new TruckData(1403,"head_SP4_1","cargo_00_SP4_1",-1.8f,-2.7f,-1f,-2f,-28,-17,5,18,new int[]{64,74,86,100,116,116,116},new int[]{1366,1624,1931,2296,2730,2730,2730},new int[]{38,47,58,72,90,90,90},new long[]{1100699,275175,550350,1100699,2201397,3302095,4402793},new long[]{0,15,30,60,119,179,238},new int[]{0,60,0,150,0,0,0},new int[]{0,70,100,0,200,0,0},new int[]{0,0,90,120,150,0,0},new int[]{0,80,120,160,200,0,0},new long[]{19817,24771,34679,54496,94129,153579,232846},60401),
					new TruckData(1501,"head_J_2","cargo_04_E_2",-1.5f,-2.2f,-0.3f,-2f,-27,-16,5,20,new int[]{66,79,94,112,133,133,133},new int[]{1350,1605,1909,2270,2700,2700,2700},new int[]{41,51,64,80,100,100,100},new long[]{1099791,274948,549896,1099791,2199582,3299373,4399164},new long[]{0,14,28,55,109,164,218},new int[]{0,70,0,0,0,0,0},new int[]{0,60,120,200,300,0,0},new int[]{0,50,100,200,350,0,0},new int[]{0,0,100,250,400,0,0},new long[]{18105,22631,31684,49789,85999,140314,212734},60402),
					new TruckData(1502,"head_I_1","cargo_04_B_2",-1.5f,-2.7f,-0.3f,-2f,-27,-16,5,20,new int[]{61,73,87,103,122,122,122},new int[]{1425,1695,2016,2397,2850,2850,2850},new int[]{45,56,70,87,108,108,108},new long[]{1099791,274948,549896,1099791,2199582,3299373,4399164},new long[]{0,14,28,55,110,165,220},new int[]{0,70,0,0,0,0,0},new int[]{0,60,120,200,300,0,0},new int[]{0,50,100,200,350,0,0},new int[]{0,0,100,250,400,0,0},new long[]{18243,22804,31926,50169,86655,141384,214356},60403),
					new TruckData(1503,"head_SP5_1","cargo_00_SP5_1",-0.34f,-3f,-0.3f,-2.2f,-32,-17,5,20,new int[]{66,77,90,106,125,125,125},new int[]{1380,1641,1952,2321,2760,2760,2760},new int[]{45,56,69,85,105,105,105},new long[]{1099791,303267,606533,1213065,2426129,3639193,4852257},new long[]{0,27,53,106,212,318,424},new int[]{0,70,0,0,0,0,0},new int[]{0,60,120,200,300,0,0},new int[]{0,50,100,200,350,0,0},new int[]{0,0,100,250,400,0,0},new long[]{17647,22059,30883,48530,83825,136767,207356},60404)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetTruckData t;
			public SheetEnumerator(SheetTruckData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public TruckData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public TruckData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public TruckData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 18, "truckData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/oaxazmu/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<TruckData> _rowsList = new List<TruckData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new TruckData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.TruckData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 22; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'int'
				int tmp0; _rows[i].id = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'model_h' OF TYPE 'string'
				_rows[i].model_h = (columns[1] == null) ? "" : columns[1].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'model_c' OF TYPE 'string'
				_rows[i].model_c = (columns[2] == null) ? "" : columns[2].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'offset_x' OF TYPE 'float'
				float tmp3; _rows[i].offset_x = float.TryParse( columns[3], out tmp3) ? tmp3 : 0f;
				//Variable 'offset_y' OF TYPE 'float'
				float tmp4; _rows[i].offset_y = float.TryParse( columns[4], out tmp4) ? tmp4 : 0f;
				//Variable 'offset_x2' OF TYPE 'float'
				float tmp5; _rows[i].offset_x2 = float.TryParse( columns[5], out tmp5) ? tmp5 : 0f;
				//Variable 'offset_y2' OF TYPE 'float'
				float tmp6; _rows[i].offset_y2 = float.TryParse( columns[6], out tmp6) ? tmp6 : 0f;
				//Variable 'cam_dis' OF TYPE 'int'
				int tmp7; _rows[i].cam_dis = int.TryParse( columns[7], out tmp7) ? tmp7 : 0;
				//Variable 'cam_dis_big' OF TYPE 'int'
				int tmp8; _rows[i].cam_dis_big = int.TryParse( columns[8], out tmp8) ? tmp8 : 0;
				//Variable 'max_lv' OF TYPE 'int'
				int tmp9; _rows[i].max_lv = int.TryParse( columns[9], out tmp9) ? tmp9 : 0;
				//Variable 'pieces' OF TYPE 'int'
				int tmp10; _rows[i].pieces = int.TryParse( columns[10], out tmp10) ? tmp10 : 0;
				//Variable 'speed' OF TYPE 'int[]'
				columns[11] = CleanString(columns[11]);	
				string[] tmp11 = columns[11].Split(',');
				_rows[i].speed =  (columns[11] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp11, ParseInt);
				//Variable 'fuel' OF TYPE 'int[]'
				columns[12] = CleanString(columns[12]);	
				string[] tmp12 = columns[12].Split(',');
				_rows[i].fuel =  (columns[12] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp12, ParseInt);
				//Variable 'cargo' OF TYPE 'int[]'
				columns[13] = CleanString(columns[13]);	
				string[] tmp13 = columns[13].Split(',');
				_rows[i].cargo =  (columns[13] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp13, ParseInt);
				//Variable 'gold' OF TYPE 'long[]'
				columns[14] = CleanString(columns[14]);	
				string[] tmp14 = columns[14].Split(',');
				_rows[i].gold =  (columns[14] == "") ? new long[]{} : Array.ConvertAll<string, long>(tmp14, ParseLong);
				//Variable 'cash' OF TYPE 'long[]'
				columns[15] = CleanString(columns[15]);	
				string[] tmp15 = columns[15].Split(',');
				_rows[i].cash =  (columns[15] == "") ? new long[]{} : Array.ConvertAll<string, long>(tmp15, ParseLong);
				//Variable 'parts1' OF TYPE 'int[]'
				columns[16] = CleanString(columns[16]);	
				string[] tmp16 = columns[16].Split(',');
				_rows[i].parts1 =  (columns[16] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp16, ParseInt);
				//Variable 'parts2' OF TYPE 'int[]'
				columns[17] = CleanString(columns[17]);	
				string[] tmp17 = columns[17].Split(',');
				_rows[i].parts2 =  (columns[17] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp17, ParseInt);
				//Variable 'parts3' OF TYPE 'int[]'
				columns[18] = CleanString(columns[18]);	
				string[] tmp18 = columns[18].Split(',');
				_rows[i].parts3 =  (columns[18] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp18, ParseInt);
				//Variable 'parts4' OF TYPE 'int[]'
				columns[19] = CleanString(columns[19]);	
				string[] tmp19 = columns[19].Split(',');
				_rows[i].parts4 =  (columns[19] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp19, ParseInt);
				//Variable 'resell_gold' OF TYPE 'long[]'
				columns[20] = CleanString(columns[20]);	
				string[] tmp20 = columns[20].Split(',');
				_rows[i].resell_gold =  (columns[20] == "") ? new long[]{} : Array.ConvertAll<string, long>(tmp20, ParseLong);
				//Variable 'name_id' OF TYPE 'int'
				int tmp21; _rows[i].name_id = int.TryParse( columns[21], out tmp21) ? tmp21 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class BuffData{
		public string id;
		public int string_description;
		public int string_collect;

		public BuffData(){}

		public BuffData(string id, int string_description, int string_collect){
			this.id = id;
			this.string_description = string_description;
			this.string_collect = string_collect;
		}
	}
	public class SheetBuffData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,34);
		public readonly string[] labels = new string[]{"id","string_description","string_collect"};
		private BuffData[] _rows = new BuffData[4];
		public void Init() {
			_rows = new BuffData[]{
					new BuffData("xp",41101,41131),
					new BuffData("gold",41102,41132),
					new BuffData("speed",41103,41133),
					new BuffData("gas",41104,41134)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetBuffData t;
			public SheetEnumerator(SheetBuffData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public BuffData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public BuffData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public BuffData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public BuffData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public BuffData xp{	get{ return _rows[0]; } }
		public BuffData gold{	get{ return _rows[1]; } }
		public BuffData speed{	get{ return _rows[2]; } }
		public BuffData gas{	get{ return _rows[3]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 19, "buffData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/o5dzomg/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<BuffData> _rowsList = new List<BuffData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new BuffData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.BuffData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 3; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'string_description' OF TYPE 'int'
				int tmp1; _rows[i].string_description = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'string_collect' OF TYPE 'int'
				int tmp2; _rows[i].string_collect = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class MarketList{
		public string id;
		public string nAME;
		public string google_ID;
		public string apple_ID;
		public string amazon_ID;
		public string mAC_ID;
		public string facebook_ID;

		public MarketList(){}

		public MarketList(string id, string nAME, string google_ID, string apple_ID, string amazon_ID, string mAC_ID, string facebook_ID){
			this.id = id;
			this.nAME = nAME;
			this.google_ID = google_ID;
			this.apple_ID = apple_ID;
			this.amazon_ID = amazon_ID;
			this.mAC_ID = mAC_ID;
			this.facebook_ID = facebook_ID;
		}
	}
	public class SheetMarketList: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,35);
		public readonly string[] labels = new string[]{"id","NAME","Google_ID","Apple_ID","Amazon_ID","MAC_ID","Facebook_ID"};
		private MarketList[] _rows = new MarketList[43];
		public void Init() {
			_rows = new MarketList[]{
					new MarketList("coin_001","Handful","coin_001","coin_001","coin_001","coin_001_m","coin_001"),
					new MarketList("coin_002","Two Handful","coin_002","coin_002","coin_002","coin_002_m","coin_002"),
					new MarketList("coin_003","Tower of Coins","coin_003","coin_003","coin_003","coin_003_m","coin_003"),
					new MarketList("coin_004","Coin Bag","coin_004","coin_004","coin_004","coin_004_m","coin_004"),
					new MarketList("coin_005","Crate","coin_005","coin_005","coin_005","coin_005_m","coin_005"),
					new MarketList("coin_006","Big Safe","coin_006","coin_006","coin_006","coin_006_m","coin_006"),
					new MarketList("cash_001","Stack","cash_001","cash_001","cash_001","cash_001_m","cash_001"),
					new MarketList("cash_002","Bankroll","cash_002","cash_002","cash_002","cash_002_m","cash_002"),
					new MarketList("cash_003","Tower of Cash","cash_003","cash_003","cash_003","cash_003_m","cash_003"),
					new MarketList("cash_004","Big Money","cash_004","cash_004","cash_004","cash_004_m","cash_004"),
					new MarketList("cash_005","Rich Life","cash_005","cash_005","cash_005","cash_005_m","cash_005"),
					new MarketList("cash_006","Bank Owner","cash_006","cash_006","cash_006","cash_006_m","cash_006"),
					new MarketList("beginner_001","Beginner Truck Pack","beginner_truck_pack","beginner_truck_pack","beginner_truck_pack","beginner_truck_pack_m","beginner_truck_pack"),
					new MarketList("beginner_002","Beginner Truck Mid Pack 1","beginner_truck_mid_pack","beginner_truck_mid_pack","beginner_truck_mid_pack","beginner_truck_mid_pack_m","beginner_truck_mid_pack"),
					new MarketList("beginner_003","Beginner Truck Mid Pack 2","beginner_truck_mid_pack2","beginner_truck_mid_pack2","beginner_truck_mid_pack2","beginner_truck_mid_pack_m2","beginner_truck_mid_pack2"),
					new MarketList("beginner_004","Beginner Truck Big Pack","beginner_truck_big_pack","beginner_truck_big_pack","beginner_truck_big_pack","beginner_truck_big_pack_m","beginner_truck_big_pack"),
					new MarketList("event_001","Event Truck Pack","event_truck_pack","event_truck_pack","event_truck_pack","event_truck_pack_m","event_truck_pack"),
					new MarketList("event_002","Event Truck Mid Pack 1","event_truck_mid_pack","event_truck_mid_pack","event_truck_mid_pack","event_truck_mid_pack_m","event_truck_mid_pack"),
					new MarketList("event_003","Event Truck Mid Pack 2","event_truck_mid_pack2","event_truck_mid_pack2","event_truck_mid_pack2","event_truck_mid_pack2_m","event_truck_mid_pack2"),
					new MarketList("event_004","Event Truck Big Pack","event_truck_big_pack","event_truck_big_pack","event_truck_big_pack","event_truck_big_pack_m","event_truck_big_pack"),
					new MarketList("advanced_001","Advanced Truck Pack","advanced_truck_pack","advanced_truck_pack","advanced_truck_pack","advanced_truck_pack_m","advanced_truck_pack"),
					new MarketList("advanced_002","Advanced Truck Mid Pack 1","advanced_truck_mid_pack","advanced_truck_mid_pack","advanced_truck_mid_pack","advanced_truck_mid_pack_m","advanced_truck_mid_pack"),
					new MarketList("advanced_003","Advanced Truck Mid Pack 2","advanced_truck_mid_pack2","advanced_truck_mid_pack2","advanced_truck_mid_pack2","advanced_truck_mid_pack2_m","advanced_truck_mid_pack2"),
					new MarketList("advanced_004","Advanced Truck Big Pack","advanced_truck_big_pack","advanced_truck_big_pack","advanced_truck_big_pack","advanced_truck_big_pack_m","advanced_truck_big_pack"),
					new MarketList("big_001","Big Truck Pack","big_truck_pack","big_truck_pack","big_truck_pack","big_truck_pack_m","big_truck_pack"),
					new MarketList("big_002","Big Truck Mid Pack 1","big_truck_mid_pack","big_truck_mid_pack","big_truck_mid_pack","big_truck_mid_pack_m","big_truck_mid_pack"),
					new MarketList("big_003","Big Truck Mid Pack 2","big_truck_mid_pack2","big_truck_mid_pack2","big_truck_mid_pack2","big_truck_mid_pack2_m","big_truck_mid_pack2"),
					new MarketList("big_004","Big Truck Big Pack","big_truck_big_pack","big_truck_big_pack","big_truck_big_pack","big_truck_big_pack_m","big_truck_big_pack"),
					new MarketList("best_001","Best Truck Pack","best_truck_pack","best_truck_pack","best_truck_pack","best_truck_pack_m","best_truck_pack"),
					new MarketList("best_002","Best Truck Mid Pack 1","best_truck_mid_pack","best_truck_mid_pack","best_truck_mid_pack","best_truck_mid_pack_m","best_truck_mid_pack"),
					new MarketList("best_003","Best Truck Mid Pack 2","best_truck_mid_pack2","best_truck_mid_pack2","best_truck_mid_pack2","best_truck_mid_pack2_m","best_truck_mid_pack2"),
					new MarketList("best_004","Best Truck Big Pack","best_truck_big_pack","best_truck_big_pack","best_truck_big_pack","best_truck_big_pack_m","best_truck_big_pack"),
					new MarketList("super_001","Super Truck Pack","super_truck_pack","super_truck_pack","super_truck_pack","super_truck_pack_m","super_truck_pack"),
					new MarketList("super_002","Super Truck Mid Pack 1","super_truck_mid_pack","super_truck_mid_pack","super_truck_mid_pack","super_truck_mid_pack_m","super_truck_mid_pack"),
					new MarketList("super_003","Super Truck Mid Pack 2","super_truck_mid_pack2","super_truck_mid_pack2","super_truck_mid_pack2","super_truck_mid_pack2_m","super_truck_mid_pack2"),
					new MarketList("super_004","Super Truck Big Pack","super_truck_big_pack","super_truck_big_pack","super_truck_big_pack","super_truck_big_pack_m","super_truck_big_pack"),
					new MarketList("sp_crate_10","Special Crate 10","special_crate_10","special_crate_10","special_crate_10","special_crate_10","special_crate_10"),
					new MarketList("sp_crate_50","Special Crate 50","special_crate_50","special_crate_50","special_crate_50","special_crate_50","special_crate_50"),
					new MarketList("nm_crate_10","Normal Crate 10","normal_crate_10","normal_crate_10","normal_crate_10","normal_crate_10","normal_crate_10"),
					new MarketList("nm_crate_50","Normal Crate 50","normal_crate_50","normal_crate_50","normal_crate_50","normal_crate_50","normal_crate_50"),
					new MarketList("booster_pack1","Booster Pack 1","booster_pack1","booster_pack1","booster_pack1","booster_pack1","booster_pack1"),
					new MarketList("booster_pack2","Booster Pack 2","booster_pack2","booster_pack2","booster_pack2","booster_pack2","booster_pack2"),
					new MarketList("booster_pack3","Booster Pack 3","booster_pack3","booster_pack3","booster_pack3","booster_pack3","booster_pack3")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetMarketList t;
			public SheetEnumerator(SheetMarketList t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public MarketList this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public MarketList this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public MarketList[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public MarketList Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public MarketList coin_001{	get{ return _rows[0]; } }
		public MarketList coin_002{	get{ return _rows[1]; } }
		public MarketList coin_003{	get{ return _rows[2]; } }
		public MarketList coin_004{	get{ return _rows[3]; } }
		public MarketList coin_005{	get{ return _rows[4]; } }
		public MarketList coin_006{	get{ return _rows[5]; } }
		public MarketList cash_001{	get{ return _rows[6]; } }
		public MarketList cash_002{	get{ return _rows[7]; } }
		public MarketList cash_003{	get{ return _rows[8]; } }
		public MarketList cash_004{	get{ return _rows[9]; } }
		public MarketList cash_005{	get{ return _rows[10]; } }
		public MarketList cash_006{	get{ return _rows[11]; } }
		public MarketList beginner_001{	get{ return _rows[12]; } }
		public MarketList beginner_002{	get{ return _rows[13]; } }
		public MarketList beginner_003{	get{ return _rows[14]; } }
		public MarketList beginner_004{	get{ return _rows[15]; } }
		public MarketList event_001{	get{ return _rows[16]; } }
		public MarketList event_002{	get{ return _rows[17]; } }
		public MarketList event_003{	get{ return _rows[18]; } }
		public MarketList event_004{	get{ return _rows[19]; } }
		public MarketList advanced_001{	get{ return _rows[20]; } }
		public MarketList advanced_002{	get{ return _rows[21]; } }
		public MarketList advanced_003{	get{ return _rows[22]; } }
		public MarketList advanced_004{	get{ return _rows[23]; } }
		public MarketList big_001{	get{ return _rows[24]; } }
		public MarketList big_002{	get{ return _rows[25]; } }
		public MarketList big_003{	get{ return _rows[26]; } }
		public MarketList big_004{	get{ return _rows[27]; } }
		public MarketList best_001{	get{ return _rows[28]; } }
		public MarketList best_002{	get{ return _rows[29]; } }
		public MarketList best_003{	get{ return _rows[30]; } }
		public MarketList best_004{	get{ return _rows[31]; } }
		public MarketList super_001{	get{ return _rows[32]; } }
		public MarketList super_002{	get{ return _rows[33]; } }
		public MarketList super_003{	get{ return _rows[34]; } }
		public MarketList super_004{	get{ return _rows[35]; } }
		public MarketList sp_crate_10{	get{ return _rows[36]; } }
		public MarketList sp_crate_50{	get{ return _rows[37]; } }
		public MarketList nm_crate_10{	get{ return _rows[38]; } }
		public MarketList nm_crate_50{	get{ return _rows[39]; } }
		public MarketList booster_pack1{	get{ return _rows[40]; } }
		public MarketList booster_pack2{	get{ return _rows[41]; } }
		public MarketList booster_pack3{	get{ return _rows[42]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 20, "marketList", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/orgl2ez/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<MarketList> _rowsList = new List<MarketList>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new MarketList());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.MarketList();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 7; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'nAME' OF TYPE 'string'
				_rows[i].nAME = (columns[1] == null) ? "" : columns[1].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'google_ID' OF TYPE 'string'
				_rows[i].google_ID = (columns[2] == null) ? "" : columns[2].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'apple_ID' OF TYPE 'string'
				_rows[i].apple_ID = (columns[3] == null) ? "" : columns[3].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'amazon_ID' OF TYPE 'string'
				_rows[i].amazon_ID = (columns[4] == null) ? "" : columns[4].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'mAC_ID' OF TYPE 'string'
				_rows[i].mAC_ID = (columns[5] == null) ? "" : columns[5].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'facebook_ID' OF TYPE 'string'
				_rows[i].facebook_ID = (columns[6] == null) ? "" : columns[6].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class IAPData{
		public string name;
		public DatasTypes.MarketList iap_id;
		public DatasTypes.RewardData item_type;
		public string reward_id;
		public int count;
		public int price_usd;
		public int icon;
		public int string_name;
		public int string_desc;

		public IAPData(){}

		public IAPData(string name, DatasTypes.MarketList iap_id, DatasTypes.RewardData item_type, string reward_id, int count, int price_usd, int icon, int string_name, int string_desc){
			this.name = name;
			this.iap_id = iap_id;
			this.item_type = item_type;
			this.reward_id = reward_id;
			this.count = count;
			this.price_usd = price_usd;
			this.icon = icon;
			this.string_name = string_name;
			this.string_desc = string_desc;
		}
	}
	public class SheetIAPData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,36);
		public readonly string[] labels = new string[]{"name","marketList iap_id","rewardData item_type","string reward_id","count","price_usd","icon","int string_name","int string_desc"};
		private IAPData[] _rows = new IAPData[10];
		public void Init() {
			_rows = new IAPData[]{
					new IAPData("cash_4",Datas.marketList.cash_004,Datas.rewardData.cash,"",130,10,4,20754,0),
					new IAPData("cash_6",Datas.marketList.cash_006,Datas.rewardData.cash,"",1500,100,6,20756,0),
					new IAPData("cash_1",Datas.marketList.cash_001,Datas.rewardData.cash,"",10,1,1,20751,0),
					new IAPData("cash_2",Datas.marketList.cash_002,Datas.rewardData.cash,"",33,3,2,20752,0),
					new IAPData("cash_3",Datas.marketList.cash_003,Datas.rewardData.cash,"",60,5,3,20753,0),
					new IAPData("cash_5",Datas.marketList.cash_005,Datas.rewardData.cash,"",700,50,5,20755,0),
					new IAPData("sp_crate_10",Datas.marketList.sp_crate_10,Datas.rewardData.crate,"crate2",10,3,1,20757,20761),
					new IAPData("sp_crate_50",Datas.marketList.sp_crate_50,Datas.rewardData.crate,"crate2",50,10,2,20758,20762),
					new IAPData("nm_crate_10",Datas.marketList.nm_crate_10,Datas.rewardData.crate,"crate1",10,1,1,20759,20763),
					new IAPData("nm_crate_50",Datas.marketList.nm_crate_50,Datas.rewardData.crate,"crate1",50,5,2,20760,20764)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetIAPData t;
			public SheetEnumerator(SheetIAPData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public IAPData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public IAPData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].name == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].name == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public IAPData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public IAPData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public IAPData cash_4{	get{ return _rows[0]; } }
		public IAPData cash_6{	get{ return _rows[1]; } }
		public IAPData cash_1{	get{ return _rows[2]; } }
		public IAPData cash_2{	get{ return _rows[3]; } }
		public IAPData cash_3{	get{ return _rows[4]; } }
		public IAPData cash_5{	get{ return _rows[5]; } }
		public IAPData sp_crate_10{	get{ return _rows[6]; } }
		public IAPData sp_crate_50{	get{ return _rows[7]; } }
		public IAPData nm_crate_10{	get{ return _rows[8]; } }
		public IAPData nm_crate_50{	get{ return _rows[9]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 21, "iAPData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/osaly1v/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<IAPData> _rowsList = new List<IAPData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new IAPData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.IAPData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 9; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'name' OF TYPE 'string'
				_rows[i].name = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'iap_id' OF TYPE 'DatasTypes.MarketList'
				_rows[i].iap_id = Datas.marketList[ columns[1].Split('.').Last() ];
				//Variable 'item_type' OF TYPE 'DatasTypes.RewardData'
				_rows[i].item_type = Datas.rewardData[ columns[2].Split('.').Last() ];
				//Variable 'reward_id' OF TYPE 'string'
				_rows[i].reward_id = (columns[3] == null) ? "" : columns[3].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'count' OF TYPE 'int'
				int tmp4; _rows[i].count = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'price_usd' OF TYPE 'int'
				int tmp5; _rows[i].price_usd = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
				//Variable 'icon' OF TYPE 'int'
				int tmp6; _rows[i].icon = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
				//Variable 'string_name' OF TYPE 'int'
				int tmp7; _rows[i].string_name = int.TryParse( columns[7], out tmp7) ? tmp7 : 0;
				//Variable 'string_desc' OF TYPE 'int'
				int tmp8; _rows[i].string_desc = int.TryParse( columns[8], out tmp8) ? tmp8 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class CoinShopData{
		public string id;
		public int bonus_percent;
		public DatasTypes.RewardData price_type;
		public int price_count;
		public int string_name;
		public int icon;

		public CoinShopData(){}

		public CoinShopData(string id, int bonus_percent, DatasTypes.RewardData price_type, int price_count, int string_name, int icon){
			this.id = id;
			this.bonus_percent = bonus_percent;
			this.price_type = price_type;
			this.price_count = price_count;
			this.string_name = string_name;
			this.icon = icon;
		}
	}
	public class SheetCoinShopData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,37);
		public readonly string[] labels = new string[]{"id","bonus_percent","rewardData price_type","price_count","string_name","icon"};
		private CoinShopData[] _rows = new CoinShopData[6];
		public void Init() {
			_rows = new CoinShopData[]{
					new CoinShopData("coin_4",30,Datas.rewardData.cash,100,20704,4),
					new CoinShopData("coin_6",50,Datas.rewardData.cash,1000,20706,6),
					new CoinShopData("coin_1",0,Datas.rewardData.cash,10,20701,1),
					new CoinShopData("coin_2",10,Datas.rewardData.cash,30,20702,2),
					new CoinShopData("coin_3",20,Datas.rewardData.cash,50,20703,3),
					new CoinShopData("coin_5",40,Datas.rewardData.cash,500,20705,5)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCoinShopData t;
			public SheetEnumerator(SheetCoinShopData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public CoinShopData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public CoinShopData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public CoinShopData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public CoinShopData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public CoinShopData coin_4{	get{ return _rows[0]; } }
		public CoinShopData coin_6{	get{ return _rows[1]; } }
		public CoinShopData coin_1{	get{ return _rows[2]; } }
		public CoinShopData coin_2{	get{ return _rows[3]; } }
		public CoinShopData coin_3{	get{ return _rows[4]; } }
		public CoinShopData coin_5{	get{ return _rows[5]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 22, "coinShopData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ooxcp07/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<CoinShopData> _rowsList = new List<CoinShopData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new CoinShopData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.CoinShopData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 6; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'bonus_percent' OF TYPE 'int'
				int tmp1; _rows[i].bonus_percent = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'price_type' OF TYPE 'DatasTypes.RewardData'
				_rows[i].price_type = Datas.rewardData[ columns[2].Split('.').Last() ];
				//Variable 'price_count' OF TYPE 'int'
				int tmp3; _rows[i].price_count = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'string_name' OF TYPE 'int'
				int tmp4; _rows[i].string_name = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'icon' OF TYPE 'int'
				int tmp5; _rows[i].icon = int.TryParse( columns[5], out tmp5) ? tmp5 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class EventPackData{
		public int start_lv;
		public int end_level;
		public int truck_id;
		public int gold;
		public int cash;
		public DatasTypes.MarketList iap_id;

		public EventPackData(){}

		public EventPackData(int start_lv, int end_level, int truck_id, int gold, int cash, DatasTypes.MarketList iap_id){
			this.start_lv = start_lv;
			this.end_level = end_level;
			this.truck_id = truck_id;
			this.gold = gold;
			this.cash = cash;
			this.iap_id = iap_id;
		}
	}
	public class SheetEventPackData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,38);
		public readonly string[] labels = new string[]{"start_lv","end_level","truck_id","int gold","int cash","marketList iap_id"};
		private EventPackData[] _rows = new EventPackData[212];
		public void Init() {
			_rows = new EventPackData[]{
					new EventPackData(299,300,1601,2000,30,Datas.marketList.beginner_001),
					new EventPackData(299,300,1602,2000,50,Datas.marketList.beginner_002),
					new EventPackData(299,300,1603,2000,30,Datas.marketList.beginner_003),
					new EventPackData(299,300,1604,2000,100,Datas.marketList.beginner_004),
					new EventPackData(299,300,1605,3000,30,Datas.marketList.beginner_001),
					new EventPackData(299,300,1606,3000,50,Datas.marketList.beginner_002),
					new EventPackData(299,300,1607,3000,30,Datas.marketList.beginner_003),
					new EventPackData(299,300,1608,3000,100,Datas.marketList.beginner_004),
					new EventPackData(299,300,1609,4000,30,Datas.marketList.beginner_001),
					new EventPackData(299,300,1610,4000,50,Datas.marketList.beginner_002),
					new EventPackData(299,300,1611,4000,30,Datas.marketList.beginner_003),
					new EventPackData(299,300,1612,4000,100,Datas.marketList.beginner_004),
					new EventPackData(299,300,1613,5000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1614,5000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1615,5000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1616,5000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1617,6000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1618,6000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1619,6000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1620,6000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1621,7000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1622,7000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1623,7000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1624,7000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1625,8000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1626,8000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1627,8000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1628,8000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1629,9000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1630,9000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1631,9000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1632,9000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1633,10000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1634,10000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1635,10000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1636,10000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1637,12000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1638,12000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1639,12000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1640,12000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1641,14000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1642,14000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1643,14000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1644,14000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1645,16000,30,Datas.marketList.event_001),
					new EventPackData(299,300,1646,16000,50,Datas.marketList.event_002),
					new EventPackData(299,300,1647,16000,30,Datas.marketList.event_003),
					new EventPackData(299,300,1648,16000,100,Datas.marketList.event_004),
					new EventPackData(299,300,1649,18000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1650,18000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1651,18000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1652,18000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1653,21000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1654,21000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1655,21000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1656,21000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1657,23000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1658,23000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1659,23000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1660,23000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1661,25000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1662,25000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1663,25000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1664,25000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1665,28000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1666,28000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1667,28000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1668,28000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1669,30000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1670,30000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1671,30000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1672,30000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1673,33000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1674,33000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1675,33000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1676,33000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1677,36000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1678,36000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1679,36000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1680,36000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1681,39000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1682,39000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1683,39000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1684,39000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1685,42000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1686,42000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1687,42000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1688,42000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1689,45000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1690,45000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1691,45000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1692,45000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1693,49000,50,Datas.marketList.advanced_001),
					new EventPackData(299,300,1694,49000,100,Datas.marketList.advanced_002),
					new EventPackData(299,300,1695,49000,50,Datas.marketList.advanced_003),
					new EventPackData(299,300,1696,49000,150,Datas.marketList.advanced_004),
					new EventPackData(299,300,1697,52000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1698,52000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1699,52000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1700,52000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1701,57000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1702,57000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1703,57000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1704,57000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1705,61000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1706,61000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1707,61000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1708,61000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1709,66000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1710,66000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1711,66000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1712,66000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1713,71000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1714,71000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1715,71000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1716,71000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1717,76000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1718,76000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1719,76000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1720,76000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1721,81000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1722,81000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1723,81000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1724,81000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1725,87000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1726,87000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1727,87000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1728,87000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1729,93000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1730,93000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1731,93000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1732,93000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1733,100000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1734,100000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1735,100000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1736,100000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1737,107000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1738,107000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1739,107000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1740,107000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1741,114000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1742,114000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1743,114000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1744,114000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1745,121000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1746,121000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1747,121000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1748,121000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1749,127000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1750,127000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1751,127000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1752,127000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1753,133000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1754,133000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1755,133000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1756,133000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1757,140000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1758,140000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1759,140000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1760,140000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1761,146000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1762,146000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1763,146000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1764,146000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1765,153000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1766,153000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1767,153000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1768,153000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1769,160000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1770,160000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1771,160000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1772,160000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1773,167000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1774,167000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1775,167000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1776,167000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1777,175000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1778,175000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1779,175000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1780,175000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1781,183000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1782,183000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1783,183000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1784,183000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1785,191000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1786,191000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1787,191000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1788,191000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1789,198000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1790,198000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1791,198000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1792,198000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1793,201000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1794,201000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1795,201000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1796,201000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1797,204000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1798,204000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1799,204000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1800,204000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1801,207000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1802,207000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1803,207000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1804,207000,300,Datas.marketList.best_004),
					new EventPackData(299,300,1805,210000,50,Datas.marketList.big_001),
					new EventPackData(299,300,1806,210000,100,Datas.marketList.big_002),
					new EventPackData(299,300,1807,210000,50,Datas.marketList.big_003),
					new EventPackData(299,300,1808,210000,150,Datas.marketList.big_004),
					new EventPackData(299,300,1809,212000,100,Datas.marketList.best_001),
					new EventPackData(299,300,1810,212000,200,Datas.marketList.best_002),
					new EventPackData(299,300,1901,212000,100,Datas.marketList.best_003),
					new EventPackData(299,300,1902,212000,300,Datas.marketList.best_004)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetEventPackData t;
			public SheetEnumerator(SheetEventPackData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public EventPackData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public EventPackData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public EventPackData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 23, "eventPackData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/o4038u9/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<EventPackData> _rowsList = new List<EventPackData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new EventPackData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.EventPackData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 6; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'start_lv' OF TYPE 'int'
				int tmp0; _rows[i].start_lv = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'end_level' OF TYPE 'int'
				int tmp1; _rows[i].end_level = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'truck_id' OF TYPE 'int'
				int tmp2; _rows[i].truck_id = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'gold' OF TYPE 'int'
				int tmp3; _rows[i].gold = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'cash' OF TYPE 'int'
				int tmp4; _rows[i].cash = int.TryParse( columns[4], out tmp4) ? tmp4 : 0;
				//Variable 'iap_id' OF TYPE 'DatasTypes.MarketList'
				_rows[i].iap_id = Datas.marketList[ columns[5].Split('.').Last() ];
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class RoadCostData{
		public int sequence;
		public long road_cost;
		public int road_cash;

		public RoadCostData(){}

		public RoadCostData(int sequence, long road_cost, int road_cash){
			this.sequence = sequence;
			this.road_cost = road_cost;
			this.road_cash = road_cash;
		}
	}
	public class SheetRoadCostData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,40);
		public readonly string[] labels = new string[]{"Sequence","long road_cost","int road_cash"};
		private RoadCostData[] _rows = new RoadCostData[100];
		public void Init() {
			_rows = new RoadCostData[]{
					new RoadCostData(1,1000,1),
					new RoadCostData(2,1000,1),
					new RoadCostData(3,1000,1),
					new RoadCostData(4,1000,1),
					new RoadCostData(5,1000,1),
					new RoadCostData(6,1000,1),
					new RoadCostData(7,1000,1),
					new RoadCostData(8,1000,1),
					new RoadCostData(9,1000,1),
					new RoadCostData(10,1000,1),
					new RoadCostData(11,1400,1),
					new RoadCostData(12,1700,2),
					new RoadCostData(13,2000,2),
					new RoadCostData(14,2400,2),
					new RoadCostData(15,2800,3),
					new RoadCostData(16,3100,3),
					new RoadCostData(17,4200,4),
					new RoadCostData(18,4900,5),
					new RoadCostData(19,5400,5),
					new RoadCostData(20,6100,6),
					new RoadCostData(21,6800,7),
					new RoadCostData(22,7400,7),
					new RoadCostData(23,8300,8),
					new RoadCostData(24,9400,9),
					new RoadCostData(25,10100,10),
					new RoadCostData(26,11200,11),
					new RoadCostData(27,12300,12),
					new RoadCostData(28,13500,14),
					new RoadCostData(29,14800,15),
					new RoadCostData(30,16400,16),
					new RoadCostData(31,17600,18),
					new RoadCostData(32,19200,19),
					new RoadCostData(33,21400,21),
					new RoadCostData(34,22900,23),
					new RoadCostData(35,24900,25),
					new RoadCostData(36,27300,27),
					new RoadCostData(37,29100,29),
					new RoadCostData(38,31600,32),
					new RoadCostData(39,34800,35),
					new RoadCostData(40,37000,37),
					new RoadCostData(41,40100,40),
					new RoadCostData(42,43700,44),
					new RoadCostData(43,46100,46),
					new RoadCostData(44,51600,52),
					new RoadCostData(45,55800,56),
					new RoadCostData(46,59200,59),
					new RoadCostData(47,63900,64),
					new RoadCostData(48,68900,69),
					new RoadCostData(49,73000,73),
					new RoadCostData(50,79000,79),
					new RoadCostData(51,85000,85),
					new RoadCostData(52,90000,90),
					new RoadCostData(53,96800,97),
					new RoadCostData(54,104100,104),
					new RoadCostData(55,112400,112),
					new RoadCostData(56,118700,119),
					new RoadCostData(57,127500,128),
					new RoadCostData(58,137000,137),
					new RoadCostData(59,144500,145),
					new RoadCostData(60,155200,155),
					new RoadCostData(61,167000,167),
					new RoadCostData(62,176200,176),
					new RoadCostData(63,188900,189),
					new RoadCostData(64,202400,202),
					new RoadCostData(65,213300,213),
					new RoadCostData(66,228500,229),
					new RoadCostData(67,243900,244),
					new RoadCostData(68,256200,256),
					new RoadCostData(69,273300,273),
					new RoadCostData(70,291600,292),
					new RoadCostData(71,306100,306),
					new RoadCostData(72,326600,327),
					new RoadCostData(73,348300,348),
					new RoadCostData(74,365500,366),
					new RoadCostData(75,389600,390),
					new RoadCostData(76,415400,415),
					new RoadCostData(77,435700,436),
					new RoadCostData(78,464300,464),
					new RoadCostData(79,494900,495),
					new RoadCostData(80,519000,519),
					new RoadCostData(81,553000,553),
					new RoadCostData(82,589200,589),
					new RoadCostData(83,617900,618),
					new RoadCostData(84,658100,658),
					new RoadCostData(85,700700,701),
					new RoadCostData(86,734700,735),
					new RoadCostData(87,782200,782),
					new RoadCostData(88,832600,833),
					new RoadCostData(89,872900,873),
					new RoadCostData(90,929000,929),
					new RoadCostData(91,988600,989),
					new RoadCostData(92,1035900,1036),
					new RoadCostData(93,1102100,1102),
					new RoadCostData(94,1102100,1102),
					new RoadCostData(95,1102100,1102),
					new RoadCostData(96,1102100,1102),
					new RoadCostData(97,1102100,1102),
					new RoadCostData(98,1102100,1102),
					new RoadCostData(99,1102100,1102),
					new RoadCostData(100,1102100,1102)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetRoadCostData t;
			public SheetEnumerator(SheetRoadCostData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public RoadCostData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public RoadCostData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public RoadCostData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 24, "roadCostData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/o8vyubh/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<RoadCostData> _rowsList = new List<RoadCostData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new RoadCostData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.RoadCostData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 3; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'sequence' OF TYPE 'int'
				int tmp0; _rows[i].sequence = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'road_cost' OF TYPE 'long'
				long tmp1; _rows[i].road_cost = long.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'road_cash' OF TYPE 'int'
				int tmp2; _rows[i].road_cash = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class RandomBox{
		public string id;
		public int model;
		public int slot_min;
		public int slot_max;
		public DatasTypes.RewardData[] reward_type;
		public string[] type_index;
		public int[] reward_type_rate;
		public int[] reward_count_min;
		public int[] reward_count_max;

		public RandomBox(){}

		public RandomBox(string id, int model, int slot_min, int slot_max, DatasTypes.RewardData[] reward_type, string[] type_index, int[] reward_type_rate, int[] reward_count_min, int[] reward_count_max){
			this.id = id;
			this.model = model;
			this.slot_min = slot_min;
			this.slot_max = slot_max;
			this.reward_type = reward_type;
			this.type_index = type_index;
			this.reward_type_rate = reward_type_rate;
			this.reward_count_min = reward_count_min;
			this.reward_count_max = reward_count_max;
		}
	}
	public class SheetRandomBox: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,41);
		public readonly string[] labels = new string[]{"id","model","slot_min","slot_max","rewardData[] reward_type","string[] type_index","int[] reward_type_rate","int[] reward_count_min","int[] reward_count_max"};
		private RandomBox[] _rows = new RandomBox[5];
		public void Init() {
			_rows = new RandomBox[]{
					new RandomBox("random_box1",1,3,3,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.crate, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.booster, Datas.rewardData.booster, Datas.rewardData.booster },new string[]{"0","0","crate1","parts1","parts2","parts3","parts4","speed","gold","fuel"},new int[]{50,50,25,10,10,10,10,25,25,25},new int[]{250,3,1,5,5,5,5,30,30,30},new int[]{500,6,2,10,10,10,10,30,30,30}),
					new RandomBox("random_box2",2,3,3,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.crate, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.booster, Datas.rewardData.booster, Datas.rewardData.booster },new string[]{"0","0","crate1","parts1","parts2","parts3","parts4","speed","gold","fuel"},new int[]{50,50,25,10,10,10,10,25,25,25},new int[]{500,5,2,10,10,10,10,30,30,30},new int[]{1000,10,4,20,20,20,20,30,30,30}),
					new RandomBox("random_box3",3,3,3,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.crate, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.booster, Datas.rewardData.booster, Datas.rewardData.booster },new string[]{"0","0","crate1","parts1","parts2","parts3","parts4","speed","gold","fuel"},new int[]{50,50,25,10,10,10,10,25,25,25},new int[]{1000,10,3,15,15,15,15,30,30,30},new int[]{2000,20,6,30,30,30,30,30,30,30}),
					new RandomBox("random_box4",4,4,4,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.crate, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.booster, Datas.rewardData.booster, Datas.rewardData.booster },new string[]{"0","0","crate2","parts1","parts2","parts3","parts4","speed","gold","fuel"},new int[]{100,100,100,0,0,0,0,10,10,10},new int[]{2500,20,3,20,20,20,20,120,120,120},new int[]{5000,40,6,40,40,40,40,120,120,120}),
					new RandomBox("random_box5",5,4,4,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.crate, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.parts, Datas.rewardData.booster, Datas.rewardData.booster, Datas.rewardData.booster },new string[]{"0","0","crate2","parts1","parts2","parts3","parts4","speed","gold","fuel"},new int[]{100,100,100,0,0,0,0,10,10,10},new int[]{5000,30,6,25,25,25,25,240,240,240},new int[]{10000,60,12,50,50,50,50,240,240,240})
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetRandomBox t;
			public SheetEnumerator(SheetRandomBox t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public RandomBox this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public RandomBox this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public RandomBox[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public RandomBox Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public RandomBox random_box1{	get{ return _rows[0]; } }
		public RandomBox random_box2{	get{ return _rows[1]; } }
		public RandomBox random_box3{	get{ return _rows[2]; } }
		public RandomBox random_box4{	get{ return _rows[3]; } }
		public RandomBox random_box5{	get{ return _rows[4]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 25, "randomBox", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/oijkh9l/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<RandomBox> _rowsList = new List<RandomBox>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new RandomBox());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.RandomBox();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 9; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'model' OF TYPE 'int'
				int tmp1; _rows[i].model = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'slot_min' OF TYPE 'int'
				int tmp2; _rows[i].slot_min = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'slot_max' OF TYPE 'int'
				int tmp3; _rows[i].slot_max = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'reward_type' OF TYPE 'DatasTypes.RewardData[]'
				List<RewardData> tmp4 = new List<RewardData>();
				foreach(string s in columns[4].Split(',')) {
					tmp4.Add( Datas.rewardData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].reward_type = tmp4.ToArray();
				//Variable 'type_index' OF TYPE 'string[]'
				columns[5] = CleanString(columns[5]);
				_rows[i].type_index = (columns[5] == "") ? new string[]{} : columns[5].Split(',');
				//Variable 'reward_type_rate' OF TYPE 'int[]'
				columns[6] = CleanString(columns[6]);	
				string[] tmp6 = columns[6].Split(',');
				_rows[i].reward_type_rate =  (columns[6] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp6, ParseInt);
				//Variable 'reward_count_min' OF TYPE 'int[]'
				columns[7] = CleanString(columns[7]);	
				string[] tmp7 = columns[7].Split(',');
				_rows[i].reward_count_min =  (columns[7] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp7, ParseInt);
				//Variable 'reward_count_max' OF TYPE 'int[]'
				columns[8] = CleanString(columns[8]);	
				string[] tmp8 = columns[8].Split(',');
				_rows[i].reward_count_max =  (columns[8] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp8, ParseInt);
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class Crate{
		public string id;
		public int icon;
		public int slot_min;
		public int slot_max;
		public int[] piece_type;
		public int[] reward_type_rate;
		public int reward_count_min;
		public int reward_count_max;
		public DatasTypes.RewardData price_type;
		public int price_count;
		public int title_string;
		public int des_string;

		public Crate(){}

		public Crate(string id, int icon, int slot_min, int slot_max, int[] piece_type, int[] reward_type_rate, int reward_count_min, int reward_count_max, DatasTypes.RewardData price_type, int price_count, int title_string, int des_string){
			this.id = id;
			this.icon = icon;
			this.slot_min = slot_min;
			this.slot_max = slot_max;
			this.piece_type = piece_type;
			this.reward_type_rate = reward_type_rate;
			this.reward_count_min = reward_count_min;
			this.reward_count_max = reward_count_max;
			this.price_type = price_type;
			this.price_count = price_count;
			this.title_string = title_string;
			this.des_string = des_string;
		}
	}
	public class SheetCrate: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,42);
		public readonly string[] labels = new string[]{"id","icon","slot_min","slot_max","int[] piece_type","int[] reward_type_rate","reward_count_min","reward_count_max","rewardData price_type","price_count","title_string","des_string"};
		private Crate[] _rows = new Crate[2];
		public void Init() {
			_rows = new Crate[]{
					new Crate("crate1",1,3,3,new int[]{101,201,202,301,302,303,401,402,501,502},new int[]{15,10,10,13,13,13,8,8,5,5},1,1,Datas.rewardData.cash,10,20127,20138),
					new Crate("crate2",2,3,3,new int[]{1101,1102,1103,1201,1202,1203,1204,1301,1302,1401,1402,1403,1501,1502,1503},new int[]{6,6,6,8,8,8,8,7,7,7,7,7,5,5,5},1,1,Datas.rewardData.cash,10,20126,20137)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCrate t;
			public SheetEnumerator(SheetCrate t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public Crate this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public Crate this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public Crate[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public Crate Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public Crate crate1{	get{ return _rows[0]; } }
		public Crate crate2{	get{ return _rows[1]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 26, "crate", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/oblkbm4/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<Crate> _rowsList = new List<Crate>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new Crate());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.Crate();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 12; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'id' OF TYPE 'string'
				_rows[i].id = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'icon' OF TYPE 'int'
				int tmp1; _rows[i].icon = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'slot_min' OF TYPE 'int'
				int tmp2; _rows[i].slot_min = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'slot_max' OF TYPE 'int'
				int tmp3; _rows[i].slot_max = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'piece_type' OF TYPE 'int[]'
				columns[4] = CleanString(columns[4]);	
				string[] tmp4 = columns[4].Split(',');
				_rows[i].piece_type =  (columns[4] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp4, ParseInt);
				//Variable 'reward_type_rate' OF TYPE 'int[]'
				columns[5] = CleanString(columns[5]);	
				string[] tmp5 = columns[5].Split(',');
				_rows[i].reward_type_rate =  (columns[5] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp5, ParseInt);
				//Variable 'reward_count_min' OF TYPE 'int'
				int tmp6; _rows[i].reward_count_min = int.TryParse( columns[6], out tmp6) ? tmp6 : 0;
				//Variable 'reward_count_max' OF TYPE 'int'
				int tmp7; _rows[i].reward_count_max = int.TryParse( columns[7], out tmp7) ? tmp7 : 0;
				//Variable 'price_type' OF TYPE 'DatasTypes.RewardData'
				_rows[i].price_type = Datas.rewardData[ columns[8].Split('.').Last() ];
				//Variable 'price_count' OF TYPE 'int'
				int tmp9; _rows[i].price_count = int.TryParse( columns[9], out tmp9) ? tmp9 : 0;
				//Variable 'title_string' OF TYPE 'int'
				int tmp10; _rows[i].title_string = int.TryParse( columns[10], out tmp10) ? tmp10 : 0;
				//Variable 'des_string' OF TYPE 'int'
				int tmp11; _rows[i].des_string = int.TryParse( columns[11], out tmp11) ? tmp11 : 0;
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class LuckyBoxData{
		public int index;
		public DateTime start_date;
		public DateTime end_date;
		public int price_cash;
		public DatasTypes.RewardData[] reward_type;
		public string[] reward_index;
		public int[] reward_count;
		public int[] reward_rate;

		public LuckyBoxData(){}

		public LuckyBoxData(int index, DateTime start_date, DateTime end_date, int price_cash, DatasTypes.RewardData[] reward_type, string[] reward_index, int[] reward_count, int[] reward_rate){
			this.index = index;
			this.start_date = start_date;
			this.end_date = end_date;
			this.price_cash = price_cash;
			this.reward_type = reward_type;
			this.reward_index = reward_index;
			this.reward_count = reward_count;
			this.reward_rate = reward_rate;
		}
	}
	public class SheetLuckyBoxData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,43);
		public readonly string[] labels = new string[]{"index","DateTime start_date","DateTime end_date","price_cash","rewardData[] reward_type","string[] reward_index","int[] reward_count","int[] reward_rate"};
		private LuckyBoxData[] _rows = new LuckyBoxData[24];
		public void Init() {
			_rows = new LuckyBoxData[]{
					new LuckyBoxData(0,new DateTime(2000,1,1,7,00,00),new DateTime(2000,1,11,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(1,new DateTime(2000,1,16,7,00,00),new DateTime(2000,1,23,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(2,new DateTime(2000,2,6,7,00,00),new DateTime(2000,2,13,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(3,new DateTime(2000,2,19,7,00,00),new DateTime(2000,2,26,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(4,new DateTime(2000,3,2,7,00,00),new DateTime(2000,3,12,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(5,new DateTime(2000,3,22,7,00,00),new DateTime(2000,3,29,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(6,new DateTime(2000,4,6,7,00,00),new DateTime(2000,4,13,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(7,new DateTime(2000,4,21,7,00,00),new DateTime(2000,4,28,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(8,new DateTime(2000,5,4,7,00,00),new DateTime(2000,5,11,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(9,new DateTime(2000,5,19,7,00,00),new DateTime(2000,5,29,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(10,new DateTime(2000,6,8,7,00,00),new DateTime(2000,6,15,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(11,new DateTime(2000,6,24,7,00,00),new DateTime(2000,7,1,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(12,new DateTime(2000,7,4,7,00,00),new DateTime(2000,7,11,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(13,new DateTime(2000,7,22,7,00,00),new DateTime(2000,8,1,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(14,new DateTime(2000,8,5,7,00,00),new DateTime(2000,8,12,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(15,new DateTime(2000,8,15,7,00,00),new DateTime(2000,8,22,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(16,new DateTime(2000,9,1,7,00,00),new DateTime(2000,9,8,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(17,new DateTime(2000,9,21,7,00,00),new DateTime(2000,10,1,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(18,new DateTime(2000,10,12,7,00,00),new DateTime(2000,10,19,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(19,new DateTime(2000,10,24,7,00,00),new DateTime(2000,11,1,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(20,new DateTime(2000,11,1,7,00,00),new DateTime(2000,11,8,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(21,new DateTime(2000,11,20,7,00,00),new DateTime(2000,12,1,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(22,new DateTime(2000,12,1,7,00,00),new DateTime(2000,12,8,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2}),
					new LuckyBoxData(23,new DateTime(2000,12,20,7,00,00),new DateTime(2001,1,1,7,00,00),100,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.gold, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.crate, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new string[]{"0","0","0","crate2","crate2","crate2","0","0","0"},new int[]{20000,50000,100000,2,5,10,50,100,200},new int[]{18,18,5,18,18,5,5,5,2})
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetLuckyBoxData t;
			public SheetEnumerator(SheetLuckyBoxData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public LuckyBoxData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public LuckyBoxData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public LuckyBoxData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items


		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 27, "luckyBoxData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/ospp9ap/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<LuckyBoxData> _rowsList = new List<LuckyBoxData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new LuckyBoxData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.LuckyBoxData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 8; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'index' OF TYPE 'int'
				int tmp0; _rows[i].index = int.TryParse( columns[0], out tmp0) ? tmp0 : 0;
				//Variable 'start_date' OF TYPE 'DateTime'
				Debug.Log(">> "+columns[1]);
				if( columns[1].Trim().Length > 0 ) {
					int[] tmp1 = Array.ConvertAll( columns[1].Split('/'), int.Parse);
					if(tmp1.Length == 3 && tmp1[0].ToString().Length == 4){
						_rows[i].start_date = new DateTime( tmp1[0], tmp1[1], tmp1[2] );//YYYY/MM/DD
					}else if(tmp1.Length == 3 && tmp1[2].ToString().Length == 4){
						_rows[i].start_date = new DateTime( tmp1[2], tmp1[1], tmp1[0] );//DD/MM/YYYY
					}else if(tmp1.Length == 6 && tmp1[0].ToString().Length == 4){
						_rows[i].start_date = new DateTime( tmp1[0], tmp1[1], tmp1[2], tmp1[3], tmp1[4], tmp1[5] );//DD/MM/YYYY/hh/mm/ss
					}else if(tmp1.Length == 6 && tmp1[2].ToString().Length == 4){
						_rows[i].start_date = new DateTime( tmp1[2], tmp1[1], tmp1[0], tmp1[3], tmp1[4], tmp1[5] );//DD/MM/YYYY/hh/mm/ss
					}else{
						Debug.LogError("Wrong format for DateTime cell 'columns[1]'");
					}
				}
				
				//Variable 'end_date' OF TYPE 'DateTime'
				Debug.Log(">> "+columns[2]);
				if( columns[2].Trim().Length > 0 ) {
					int[] tmp2 = Array.ConvertAll( columns[2].Split('/'), int.Parse);
					if(tmp2.Length == 3 && tmp2[0].ToString().Length == 4){
						_rows[i].end_date = new DateTime( tmp2[0], tmp2[1], tmp2[2] );//YYYY/MM/DD
					}else if(tmp2.Length == 3 && tmp2[2].ToString().Length == 4){
						_rows[i].end_date = new DateTime( tmp2[2], tmp2[1], tmp2[0] );//DD/MM/YYYY
					}else if(tmp2.Length == 6 && tmp2[0].ToString().Length == 4){
						_rows[i].end_date = new DateTime( tmp2[0], tmp2[1], tmp2[2], tmp2[3], tmp2[4], tmp2[5] );//DD/MM/YYYY/hh/mm/ss
					}else if(tmp2.Length == 6 && tmp2[2].ToString().Length == 4){
						_rows[i].end_date = new DateTime( tmp2[2], tmp2[1], tmp2[0], tmp2[3], tmp2[4], tmp2[5] );//DD/MM/YYYY/hh/mm/ss
					}else{
						Debug.LogError("Wrong format for DateTime cell 'columns[2]'");
					}
				}
				
				//Variable 'price_cash' OF TYPE 'int'
				int tmp3; _rows[i].price_cash = int.TryParse( columns[3], out tmp3) ? tmp3 : 0;
				//Variable 'reward_type' OF TYPE 'DatasTypes.RewardData[]'
				List<RewardData> tmp4 = new List<RewardData>();
				foreach(string s in columns[4].Split(',')) {
					tmp4.Add( Datas.rewardData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].reward_type = tmp4.ToArray();
				//Variable 'reward_index' OF TYPE 'string[]'
				columns[5] = CleanString(columns[5]);
				_rows[i].reward_index = (columns[5] == "") ? new string[]{} : columns[5].Split(',');
				//Variable 'reward_count' OF TYPE 'int[]'
				columns[6] = CleanString(columns[6]);	
				string[] tmp6 = columns[6].Split(',');
				_rows[i].reward_count =  (columns[6] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp6, ParseInt);
				//Variable 'reward_rate' OF TYPE 'int[]'
				columns[7] = CleanString(columns[7]);	
				string[] tmp7 = columns[7].Split(',');
				_rows[i].reward_rate =  (columns[7] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp7, ParseInt);
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}
namespace DatasTypes{
	public class TouchObjectData{
		public string index;
		public int delay_min;
		public int delay_max;
		public DatasTypes.RewardData[] reward_type;
		public int[] reward_count;
		public int[] reward_rate;
		public int[] reward_limit;

		public TouchObjectData(){}

		public TouchObjectData(string index, int delay_min, int delay_max, DatasTypes.RewardData[] reward_type, int[] reward_count, int[] reward_rate, int[] reward_limit){
			this.index = index;
			this.delay_min = delay_min;
			this.delay_max = delay_max;
			this.reward_type = reward_type;
			this.reward_count = reward_count;
			this.reward_rate = reward_rate;
			this.reward_limit = reward_limit;
		}
	}
	public class SheetTouchObjectData: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,8,5,8,0,44);
		public readonly string[] labels = new string[]{"index","delay_min","delay_max","rewardData[] reward_type","int[] reward_count","int[] reward_rate","int[] reward_limit"};
		private TouchObjectData[] _rows = new TouchObjectData[2];
		public void Init() {
			_rows = new TouchObjectData[]{
					new TouchObjectData("plane",5,15,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.cash, Datas.rewardData.cash },new int[]{50,2,3,5},new int[]{60,25,13,2},new int[]{-1,-1,-1,-1}),
					new TouchObjectData("buoy",10,20,new DatasTypes.RewardData[]{ Datas.rewardData.gold, Datas.rewardData.cash, Datas.rewardData.cash },new int[]{50,1,2},new int[]{60,30,10},new int[]{-1,-1,-1})
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetTouchObjectData t;
			public SheetEnumerator(SheetTouchObjectData t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		/// <summary>
		/// Length of rows of this sheet
		/// </summary>
		public int Length{ get{ return _rows.Length; } }
		/// <summary>
		/// Access row item by index
		/// </summary>
		public TouchObjectData this[int index]{
			get{
				return _rows[index];
			}
		}
		/// <summary>
		/// Access row item by first culumn string identifier
		/// </summary>
		public TouchObjectData this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].index == id){ return _rows[i]; }
				}
				return null;
			}
		}
		/// <summary>
		/// Does an item exist with the following key?
		/// </summary>
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].index == key){ return true; }
			}
			return false;
		}
		/// <summary>
		/// List of items
		/// </summary>
		/// <returns>Returns the internal array of items.</returns>
		public TouchObjectData[] ToArray(){
			return _rows;
		}
		/// <summary>
		/// Random item
		/// </summary>
		/// <returns>Returns a random item.</returns>
		public TouchObjectData Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}
		//Specific Items

		public TouchObjectData plane{	get{ return _rows[0]; } }
		public TouchObjectData buoy{	get{ return _rows[1]; } }

		/// <summary>
		/// Reload this sheet live from the server
		/// </summary>
		public void Reload(System.Action onCallLoaded){
			GameObject go = new GameObject("UnityReload");
			Datas_Loader.Load(go, 28, "touchObjectData", iCaroutineLoad(go,"https://spreadsheets.google.com/feeds/cells/10msuBOdPLN8g1xcP5iIKr_ydU0EKbYscct2sZlHVdgM/onzsg8t/public/basic?alt=json-in-script", onCallLoaded) );
		}
#region Reload
		private IEnumerator iCaroutineLoad(GameObject go, string url, System.Action onCallLoaded){
			WWW request = new WWW(url);
			yield return request;
			ParseLoaded(request.text, go, onCallLoaded);
		}
		private void ParseLoaded(string jsonData, GameObject go, System.Action onCallLoaded){
			int i;int j;
			JSONNode entries = JSON.Parse( GetJson(jsonData) )["feed"]["entry"];
			string[][] cells = new string[ entries.Count ][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = new string[ entries.Count ];
			}
			for (i = 0; i < entries.Count; i++) {
				string nTitle 	= entries[i]["title"]["$t"].Value;
				string nContent 	= entries[i]["content"]["$t"].Value;
				int[] colRow = GetColumnRow(nTitle);
				cells[ colRow[1] ][ colRow[0] ] = nContent;//Assign value
			}
			//Strip empty rows & commented rows
			List<string[]> keepRows = new List<string[]>();
			for (i = 0; i < cells.Length; i++) {
				if (cells[i].Length > 0 && cells[i][0] != null && string.Join("",  cells[i] ) != "" && cells[i][0].IndexOf("//") != 0){//ALL COMBINED CELLS ARE NOT EMPTY
	                keepRows.Add( cells[i] );
				}
			}
			cells = new string[keepRows.Count][];
			for (i = 0; i < cells.Length; i++) {
				cells[i] = keepRows[i];
			}
			//Strip commented columns
			List<int> keepColumns = new List<int>();
			for (i = 0; i < cells[0].Length; i++) {
				if ( cells[0][i] != null){
					if ( cells[0][i].IndexOf("//")!=0){
						keepColumns.Add( i );
					}
				}
			}
			if (keepColumns.Count != cells[0].Length){
				for (i = 0; i < cells.Length; i++) {
					List<string> row = new List<string>();
					for (j = 0; j < keepColumns.Count; j++) {
						row.Add( cells[i][ keepColumns[j] ] );
					}
					cells[i] = row.ToArray();
				}
			}
			if ((cells.Length-1) != _rows.Length) {
				//LOADED DATA AND INITIALLY COMPILED ROWS COUNT DO NOT MATCH 
				Debug.LogWarning("MeteSheets: Row mismatch, loaded data contains " + (cells.Length - 1) + " rows wheras the compiled code expects " + _rows.Length + " rows");
				List<TouchObjectData> _rowsList = new List<TouchObjectData>(_rows);
				if ((cells.Length - 1) < _rows.Length) {
					//REMOVE ROW ITEMS
					int count = _rows.Length - (cells.Length - 1);
					for (i = 0; i < count; i++) {
						_rowsList.RemoveAt(_rowsList.Count - 1);
					}
				} else if ((cells.Length - 1) > _rows.Length) {
					//ADD NEW ROW ITEMS
					int count = (cells.Length - 1) - _rows.Length;
					for (i = 0; i < count; i++) {
						_rowsList.Add(new TouchObjectData());
					}
				}
				_rows = _rowsList.ToArray();
			}
			for (i = 0; i < _rows.Length; i++) {
				//NOW PARSE AND ASSIGN LOADED STRING VALUES
				if(i > cells.Length - 1) {
					continue;
				}
				
				_rows[i] = new DatasTypes.TouchObjectData();
				string[] columns = cells[(i+1)];
				for (j = 0; j < 7; j++) {
					if (columns[j] == null){
						columns[j] = "";
					}
				}
				//Variable 'index' OF TYPE 'string'
				_rows[i].index = (columns[0] == null) ? "" : columns[0].Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\r","\n");
				//Variable 'delay_min' OF TYPE 'int'
				int tmp1; _rows[i].delay_min = int.TryParse( columns[1], out tmp1) ? tmp1 : 0;
				//Variable 'delay_max' OF TYPE 'int'
				int tmp2; _rows[i].delay_max = int.TryParse( columns[2], out tmp2) ? tmp2 : 0;
				//Variable 'reward_type' OF TYPE 'DatasTypes.RewardData[]'
				List<RewardData> tmp3 = new List<RewardData>();
				foreach(string s in columns[3].Split(',')) {
					tmp3.Add( Datas.rewardData[ s.Trim().Split('.').Last() ] );
				}
				_rows[i].reward_type = tmp3.ToArray();
				//Variable 'reward_count' OF TYPE 'int[]'
				columns[4] = CleanString(columns[4]);	
				string[] tmp4 = columns[4].Split(',');
				_rows[i].reward_count =  (columns[4] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp4, ParseInt);
				//Variable 'reward_rate' OF TYPE 'int[]'
				columns[5] = CleanString(columns[5]);	
				string[] tmp5 = columns[5].Split(',');
				_rows[i].reward_rate =  (columns[5] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp5, ParseInt);
				//Variable 'reward_limit' OF TYPE 'int[]'
				columns[6] = CleanString(columns[6]);	
				string[] tmp6 = columns[6].Split(',');
				_rows[i].reward_limit =  (columns[6] == "") ? new int[]{} : Array.ConvertAll<string, int>(tmp6, ParseInt);
			}
			Debug.Assert(this.Length > 0);
			GameObject.Destroy(go);
			if (onCallLoaded!=null){
				onCallLoaded();
			}
		}
		//Converts a string to bool
		private bool ParseBool(string inp) {
			bool value;
			inp = inp.Trim();
			if(inp == '1'.ToString()) {
				return true;
			}else if(inp == '0'.ToString()) {
				return false;
			}else if(bool.TryParse(inp, out value)) {
				return true;
			}
			//Could not parse
			value = false;
			return false;
		}
		//Converts a string to float
		private float ParseFloat(string inp){
			float result;
			if (float.TryParse (inp, out result)) {
				return result;
			}
			return 0f;
		}
		//Converts a string to int
		private int ParseInt(string inp){
			int result;
			if (int.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
		//Converts a string to long
		private long ParseLong(string inp){
			long result;
			if (long.TryParse (inp, out result)) {
				return result;
			}
			return 0;
		}
			
		//Supports A-CZ = 104 COLUMNS
		private static string[] columnNames = ("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ").Split(new char[]{','});
		private int[] GetColumnRow(string id)
		{
			string A = System.Text.RegularExpressions.Regex.Replace(id, @"[\d]","");//THE ALHABETICAL PART
			string B = System.Text.RegularExpressions.Regex.Replace(id, @"[^\d]","");//THE ROW INDEX
			int idxCol = System.Array.IndexOf<string>( columnNames, A );
			int idxRow = System.Convert.ToInt32( B )-1;//STARTS AT 0
			return new int[2]{idxCol, idxRow};
		}
		private string GetJson(string inp){
			int A = inp.IndexOf("{");
			int B = inp.LastIndexOf("}");
			if (A != -1 && B != -1){
				return inp.Substring( A, B-A+1);
			}
			return inp;
		}
		private string GetCamelCase(string inp){
			inp = inp.Trim();
			if (inp.Length == 0)
				return "";
			string append;
			string[] words = inp.Split(' ');
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < words.Length; i++) {
				string s = words[i];
				if (s.Length > 0){
					string firstLetter = s.Substring(0, 1);
					string rest = s.Substring(1, s.Length - 1);
					if (i==0) {//DON'T MODIFY FIRST CHARACTER
						append = firstLetter.ToLower() + rest;
					}else{
						append = firstLetter.ToUpper() + rest;
					}
					if(append.Trim().Length > 0) {
						sb.Append(append.Trim());
						sb.Append(" ");
					}
				}
			}	
			return (sb.ToString().Substring(0, sb.ToString().Length - 1)).Replace(" ","");
		}
		private string CleanString(string inp){
			return CleanString(inp, new string[]{" ","\t","\n","\r"} );
		}
		private string CleanString(string inp, string[] remove){
			return System.Text.RegularExpressions.Regex.Replace( inp, string.Join("|", remove), "").Trim();
		}
		
		private bool TryParseColor(string input, out Color color){
			input = input.Trim();
			System.Text.RegularExpressions.Regex regexColor = new System.Text.RegularExpressions.Regex("^[a-fA-F0-9]+$");
			string hex = input.Replace("#","");
			if (regexColor.IsMatch(hex) || input.IndexOf("#") == 0){
				if (hex.Length == 6 || hex.Length == 8){
					byte b0; 
					byte b1; 
					byte b2; 
					byte b3;
					if (byte.TryParse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b0)){
						if (byte.TryParse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b1)){
							if (byte.TryParse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b2)){
								if (input.Length == 9){
									if (byte.TryParse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber, null as System.IFormatProvider, out b3)){
										color = new Color((float)b1/255f, (float)b2/255f, (float)b3/255f, (float)b0/255f);
										return true;
									}
								}else{
									color = new Color((float)b0/255f, (float)b1/255f, (float)b2/255f);
									return true;
								}
							}	
						}
					}
				}
			}
			color = Color.black;
			return false;
		}			
		
	#endregion
	}
}