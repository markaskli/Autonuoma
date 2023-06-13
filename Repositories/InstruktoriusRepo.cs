namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Instruktorius' entity.
/// </summary>
public class InstruktoriusRepo
{
	public static List<InstruktoriusL> List()
	{
		var query = $@"SELECT 
							i.id, i.vardas, i.pavarde, i.darbo_laikas, f.adresas AS filialas
					 FROM 
					 	`{Config.TblPrefix}instruktoriai` i
						LEFT JOIN `{Config.TblPrefix}filialai` f ON f.id = i.fk_FILIALAS
					ORDER BY 
						i.id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<InstruktoriusL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.DarboLaikas = dre.From<string>("darbo_laikas");
				t.fkFilialas = dre.From<string>("filialas");
			});

		return result;
	}

	public static List<InstruktoriusL> ListForFilialas(int id)
	{
		var query = $@"SELECT
							i.id, i.vardas, i.pavarde
						FROM `{Config.TblPrefix}instruktoriai` i 
						WHERE i.fk_FILIALAS = ?Id
						ORDER BY i.id ASC";

		var drc = Sql.Query(query, args => {
			args.Add("?Id", id);
		});

		var result = 
			Sql.MapAll<InstruktoriusL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
			});

		return result;
	}

	public static InstruktoriusCE Find(int Id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}instruktoriai` WHERE id=?Id";

		var drc = 
			Sql.Query(query, args => {
				args.Add("?Id", Id);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<InstruktoriusCE>(drc, (dre, t) => {
					t.Instruktorius.Id = dre.From<int>("id");
					t.Instruktorius.Vardas = dre.From<string>("vardas");
					t.Instruktorius.Pavarde = dre.From<string>("pavarde");
					t.Instruktorius.Patirtis = dre.From<int>("patirtis");
					t.Instruktorius.DarboLaikas = dre.From<string>("darbo_laikas");
					t.Instruktorius.TelefonoNr = dre.From<string>("telefono_nr");
					t.Instruktorius.Elpastas = dre.From<string>("elpastas");
					t.Instruktorius.GimimoData = dre.From<DateTime>("gimimo_data");
					t.Instruktorius.fkFilialas = dre.From<int>("fk_FILIALAS");
				});
			
			return result;
		}

		return null;
	}

	public static void Update(InstruktoriusCE inst)
	{						
		var query = 
			$@"UPDATE `{Config.TblPrefix}instruktoriai`
			SET 
				vardas=?vardas, 
				pavarde=?pavarde,
				patirtis=?patirtis,
				darbo_laikas=?darboLaikas,
				telefono_nr=?telefonoNr,
				elpastas=?elPastas,
				gimimo_data=?gimimoData,
				fk_FILIALAS=?fk_filialas 
			WHERE 
				id=?Id";

		Sql.Update(query, args => {
			args.Add("?vardas", inst.Instruktorius.Vardas);
			args.Add("?pavarde", inst.Instruktorius.Pavarde);
			args.Add("?patirtis", inst.Instruktorius.Patirtis);
			args.Add("?darboLaikas", inst.Instruktorius.DarboLaikas);
			args.Add("?telefonoNr", inst.Instruktorius.TelefonoNr);
			args.Add("?elPastas", inst.Instruktorius.Elpastas);
			args.Add("?gimimoData", inst.Instruktorius.GimimoData);
			args.Add("?fk_filialas", inst.Instruktorius.fkFilialas);
			args.Add("?Id", inst.Instruktorius.Id);
		});				
	}

	public static void Insert(InstruktoriusCE inst)
	{							
		var query = 
			$@"INSERT INTO `{Config.TblPrefix}instruktoriai`
			(
				id,
				vardas,
				pavarde,
				patirtis,
				darbo_laikas,
				telefono_nr,
				elpastas,
				gimimo_data,
				fk_FILIALAS
			)
			VALUES(
				?Id,
				?vardas,
				?pavarde,
				?patirtis,
				?darboLaikas,
				?telefonoNr,
				?elPastas,
				?gimimoData,
				?fk_filialas
			)";

		Sql.Insert(query, args => {
			args.Add("?vardas", inst.Instruktorius.Vardas);
			args.Add("?pavarde", inst.Instruktorius.Pavarde);
			args.Add("?patirtis", inst.Instruktorius.Patirtis);
			args.Add("?darboLaikas", inst.Instruktorius.DarboLaikas);
			args.Add("?telefonoNr", inst.Instruktorius.TelefonoNr);
			args.Add("?elPastas", inst.Instruktorius.Elpastas);
			args.Add("?gimimoData", inst.Instruktorius.GimimoData);
			args.Add("?fk_filialas", inst.Instruktorius.fkFilialas);
			args.Add("?Id", inst.Instruktorius.Id);
		});				
	}

	public static void Delete(int Id)
	{			
		var query = $@"DELETE FROM `{Config.TblPrefix}instruktoriai` WHERE id=?Id";
		Sql.Delete(query, args => {
			args.Add("?Id", Id);
		});			
	}
}
