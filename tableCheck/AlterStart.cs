using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tableCheck
{
	public class AlterStart
	{
		//데이터그리드뷰에 있는 셀 조합해서 선택된셀 create문 만들기함수-안씀
		//void cellCreateSql()
		//{
		//	int rowIndex = dataGridView1.CurrentCell.RowIndex;
		//	if (rowIndex < 0) return;
		//	if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
		//	{
		//		return;
		//	}
		//	string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
		//	if (tbl == null) return;
		//	string tableComment = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
		//	if (tableComment == null) return;
		//	string fields = "";

		//	string primarykey = "";

		//	for (int i = 0; i < dataGridView2.Rows.Count; i++)
		//	{
		//		string fieldName = dataGridView2.Rows[i].Cells[0].Value.ToString();
		//		if (fieldName == null) return;

		//		if (i == 0)
		//		{
		//			primarykey = fieldName;
		//		}

		//		string columnType = dataGridView2.Rows[i].Cells[1].Value.ToString();
		//		if (columnType == null) columnType = "";
		//		else if (columnType.ToUpper().Contains("DATETIME"))
		//		{
		//			columnType = " DATETIME ";
		//		}

		//		string nullable = dataGridView2.Rows[i].Cells[5].Value.ToString();
		//		if (nullable == null) return;
		//		if (nullable == "NO") nullable = " NOT NULL";
		//		else if (nullable == "YES") nullable = " NULL";
		//		if (columnType.ToUpper().Contains("DATETIME"))
		//		{
		//			nullable = "";
		//		}

		//		string columnComment = dataGridView2.Rows[i].Cells[4].Value.ToString();
		//		if (columnComment == null) columnComment = "";
		//		else columnComment = " COMMENT '" + columnComment + "'";

		//		string utf8 = dataGridView2.Rows[i].Cells[6].Value.ToString();
		//		if (utf8 == null) utf8 = "";
		//		else if (utf8 == "") utf8 = "";
		//		else utf8 = " COLLATE '" + utf8 + "'";

		//		string def = dataGridView2.Rows[i].Cells[3].Value.ToString();
		//		if (def == null) def = "";
		//		if (columnType.ToUpper().Contains("INT"))
		//		{
		//			def = " DEFAULT 0 ";
		//		}
		//		if (columnType.ToUpper().Contains("SMALLINT") || columnType.ToUpper().Contains("DECIMAL"))
		//		{
		//			def = "0";
		//		}
		//		if (nullable.ToUpper().Contains(" NOT NULL"))
		//		{
		//			def = "";
		//		}

		//		else if (columnType.ToUpper().Contains("DATETIME"))
		//		{
		//			def = " DEFAULT NULL ";

		//		}
		//		else def = " DEFAULT '" + def + "'";

		//		fields = fields + "`" + fieldName + "` " + columnType + " " + nullable + def + columnComment + ",";
		//	}


		//	string alter = "ALTER DATABASE dawoon2 DEFAULT CHARACTER SET utf8";
		//	Create(alter);

		//	string queryCreate = "CREATE TABLE `" + tbl
		//		+ "` (" + fields + "PRIMARY KEY (`" + primarykey + "`) USING BTREE) COMMENT='" + tableComment + "'" + "DEFAULT CHARACTER SET utf8 COLLATE=" + "'utf8_general_ci'" + "ENGINE=InnoDB;"
		//		;
		//	Create(queryCreate);

		//}

		//void alterAll()
		//{
		//	for (int i = 0; i < Form1.Form.dataGridView1.Rows.Count; i++)
		//	{
		//		string tbl = dataGridView1.Rows[i].Cells[2].Value.ToString();
		//		if (tbl == null) return;

		//		//	string dropTable = "drop table " + tbl;
		//		//	Create(dropTable);
		//		string queryCreate = "SHOW CREATE TABLE " + tbl;
		//		ShowCreateTable(queryCreate);
		//	}
		//}

		//void alterOne()
		//{
		//	int rowIndex = dataGridView1.CurrentCell.RowIndex;
		//	if (rowIndex < 0) return;
		//	if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
		//	{
		//		return;
		//	}
		//	string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
		//	if (tbl == null) return;

		//	string columnType = dataGridView2.Rows[rowIndex].Cells[1].Value.ToString();
		//	if (columnType == null) columnType = "";
		//	else if (columnType.ToUpper().Contains("DATETIME"))
		//	{
		//		columnType = " DATETIME ";
		//	}

		//	string nullYN = dataGridView2.Rows[rowIndex].Cells[2].Value.ToString();
		//	if (nullYN == null) return;
		//	if (nullYN == "NO") nullYN = " NOT NULL";
		//	else if (nullYN == "YES") nullYN = " NULL";
		//	if (columnType.ToUpper().Contains("DATETIME"))
		//	{
		//		nullYN = "";
		//	}

		//	string nullable = dataGridView2.Rows[rowIndex].Cells[2].Value.ToString();
		//	if (nullable == null) return;
		//	if (nullable == "NO") nullable = " NOT NULL";
		//	else if (nullable == "YES") nullable = " NULL";
		//	if (columnType.ToUpper().Contains("DATETIME"))
		//	{
		//		nullable = "";
		//	}
		//	string def = dataGridView2.Rows[rowIndex].Cells[3].Value.ToString();
		//	if (def == null) def = "";
		//	if (columnType.ToUpper().Contains("INT"))
		//	{
		//		def = " DEFAULT 0 ";
		//	}
		//	if (columnType.ToUpper().Contains("SMALLINT") || columnType.ToUpper().Contains("DECIMAL"))
		//	{
		//		def = " 0";
		//	}

		//	else def = " DEFAULT '" + def + "'";

		//	if (nullable.ToUpper().Contains("NULL"))
		//	{
		//		def = " DEFAULT NULL";
		//	}
		//	if (nullable.ToUpper().Contains(" NOT NULL"))
		//	{
		//		def = "";
		//	}
		//	else if (columnType.ToUpper().Contains("DATETIME"))
		//	{
		//		def = " DEFAULT NULL ";

		//	}

		//	if (dataGridView2.Rows[rowIndex].Cells[7].Value == null)
		//	{
		//		string alterTable = "ALTER TABLE " + tbl + " ADD `" + dataGridView2.Rows[rowIndex].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + nullable + def;
		//		Create(alterTable);
		//	}
		//	if (dataGridView2.Rows[rowIndex].Cells[1].Value.ToString().Trim() != dataGridView2.Rows[rowIndex].Cells[8].Value.ToString().Trim())
		//	{
		//		string alterTable = "ALTER TABLE " + tbl + " MODIFY `" + dataGridView2.Rows[rowIndex].Cells[7].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + nullable + def;
		//		Create(alterTable);
		//	}
		//	if (dataGridView2.Rows[rowIndex].Cells[3].Value.ToString().Trim() != dataGridView2.Rows[rowIndex].Cells[10].Value.ToString().Trim())
		//	{
		//		string alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[rowIndex].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + nullYN + " DEFAULT '" + dataGridView2.Rows[rowIndex].Cells[3].Value.ToString() + "'";
		//		Create(alterTable);
		//	}
		//	if (dataGridView2.Rows[rowIndex].Cells[2].Value.ToString().Trim() != dataGridView2.Rows[rowIndex].Cells[9].Value.ToString().Trim())
		//	{
		//		if (dataGridView2.Rows[rowIndex].Cells[2].Value.ToString().Trim() == "NO")
		//		{
		//			string alterTable = "ALTER TABLE " + tbl + " MODIFY `" + dataGridView2.Rows[rowIndex].Cells[7].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + "NOT NULL";
		//			Create(alterTable);
		//		}
		//		else if (dataGridView2.Rows[rowIndex].Cells[2].Value.ToString().Trim() == "YES")
		//		{
		//			string alterTable = "ALTER TABLE " + tbl + " MODIFY `" + dataGridView2.Rows[rowIndex].Cells[7].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString();
		//			Create(alterTable);
		//		}
		//	}
		//	//CHANGE로 커멘트 변경할려면 컬럼도 동시에 바꿔져야됨 
		//	//ALTER TABLE `user` CHANGE `id` `id` INT( 11 ) COMMENT 'user 테이블의 id';
		//	if (dataGridView2.Rows[rowIndex].Cells[4].Value.ToString().Trim() != dataGridView2.Rows[rowIndex].Cells[11].Value.ToString().Trim())
		//	{
		//		string alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[rowIndex].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + nullable + " COMMENT '" + dataGridView2.Rows[rowIndex].Cells[4].Value.ToString() + "'";
		//		Create(alterTable);
		//	}

		//	//collate utf8로 전부 수정
		//	//ALTER TABLE 테이블명 MODIFY COLUMN 컬럼 VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_general_ci
		//	if (dataGridView2.Rows[rowIndex].Cells[5].Value.ToString().Trim() != dataGridView2.Rows[rowIndex].Cells[12].Value.ToString().Trim())
		//	{
		//		string alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[rowIndex].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + " CHARACTER SET utf8 COLLATE utf8_general_ci";
		//		Create(alterTable);
		//	}

		//	//autoincrement alter
		//	//ALTER TABLE 적용할테이블명칭 MODIFY 컬럼 INT NOT NULL AUTO_INCREMENT;
		//	if (dataGridView2.Rows[rowIndex].Cells[6].Value.ToString().Trim() != dataGridView2.Rows[rowIndex].Cells[13].Value.ToString().Trim())
		//	{
		//		string alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[rowIndex].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[rowIndex].Cells[1].Value.ToString() + nullable + "AUTO_INCREMENT";
		//		Create(alterTable);
		//	}
		//}


	}
}
