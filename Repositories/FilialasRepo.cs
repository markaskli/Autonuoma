namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;



/// <summary>
/// Database operations related to 'Aikstele' entity.
/// </summary>
public class FilialasRepo
{
	public static List<FilialasL> List()
	{
		var query = $@"SELECT 
							f.id, f.adresas, f.telefono_nr, f.darbo_laikas, m.pavadinimas AS pavadinimas
					 FROM 
					 	`{Config.TblPrefix}filialai` f
						LEFT JOIN `{Config.TblPrefix}vairavimo_mokyklos` m ON m.imones_kodas = f.fk_VAIRAVIMO_MOKYKLA
					ORDER BY 
						f.id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<FilialasL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Adresas = dre.From<string>("adresas");
				t.TelefonoNr = dre.From<string>("telefono_nr");
				t.DarboLaikas = dre.From<string>("darbo_laikas");
				t.FkVairavimoMokykla = dre.From<string>("pavadinimas");
			});

		return result;
	}

	public static List<FilialasL> ListForDarbuotojas(string id)
	{
		var query = $@"SELECT
							f.id, f.adresas, m.pavadinimas AS pavadinimas
						FROM `{Config.TblPrefix}filialai` f
							LEFT JOIN `{Config.TblPrefix}vairavimo_mokyklos` m ON m.imones_kodas = f.fk_VAIRAVIMO_MOKYKLA
						WHERE fk_VAIRAVIMO_MOKYKLA = ?Id
						ORDER BY f.id ASC";

		var drc = Sql.Query(query, args => {
			args.Add("?Id", id);
		});

        if (drc.Count > 0)
        {
            var result =
                Sql.MapAll<FilialasL>(drc, (dre, t) =>
                {
					t.Id = dre.From<int>("f.id");
					t.Adresas = dre.From<string>("f.adresas");
					t.FkVairavimoMokykla = dre.From<string>("pavadinimas");
                });

            return result;
        }

		return null;

	}

	public static FilialasCE Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}filialai` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<FilialasCE>(drc, (dre, t) => {
				t.Model.Id = dre.From<int>("id");
				t.Model.Adresas = dre.From<string>("adresas");
				t.Model.TelefonoNr = dre.From<string>("telefono_nr");
				t.Model.ElPastas = dre.From<string>("elpastas");
				t.Model.DarboLaikas = dre.From<string>("darbo_laikas");
				t.Model.FkVairavimoMokykla = dre.From<string>("fk_VAIRAVIMO_MOKYKLA");
			});

		return result;
	}


	public static void Update(FilialasCE filialias)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}filialai`
			SET
				adresas=?adresas,
				telefono_nr=?telefonoNr,
				elpastas=?elPastas,
				darbo_laikas=?darboLaikas,
				fk_VAIRAVIMO_MOKYKLA=?fk_vairavimo_mokykla
			WHERE
				id=?id";

		Sql.Update(query, args => {
			args.Add("?adresas", filialias.Model.Adresas);
			args.Add("?telefonoNr", filialias.Model.TelefonoNr);
			args.Add("?elPastas", filialias.Model.ElPastas);
			args.Add("?darboLaikas", filialias.Model.DarboLaikas);
			args.Add("?fk_vairavimo_mokykla", filialias.Model.FkVairavimoMokykla);
			args.Add("?id", filialias.Model.Id);
		});
	}

	public static void Insert(FilialasCE filialas)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}filialai`
			(
				`adresas`,
				`telefono_nr`,
				`elpastas`,
				`darbo_laikas`,
				`id`,
				`fk_VAIRAVIMO_MOKYKLA`
			)
			VALUES(
				?adresas,
				?telefonoNr,
				?elPastas,
				?darboLaikas,
				?id,
				?fk_vairavimo_mokykla
			)";

		Sql.Insert(query, args => {
			args.Add("?adresas", filialas.Model.Adresas);
			args.Add("?telefonoNr", filialas.Model.TelefonoNr);
			args.Add("?elPastas", filialas.Model.ElPastas);
			args.Add("?darboLaikas", filialas.Model.DarboLaikas);
			args.Add("?id", filialas.Model.Id);
			args.Add("?fk_vairavimo_mokykla", filialas.Model.FkVairavimoMokykla);
		});
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}filialai` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}


	public static List<FilialasL> ListForMokykla(string mokyklosKodas)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}filialai` WHERE fk_VAIRAVIMO_MOKYKLA=?mokyklosKodas ORDER BY id ASC";
		var drc =
			Sql.Query(query, args => {
				args.Add("?mokyklosKodas", mokyklosKodas);
			});

		var result = 
			Sql.MapAll<FilialasL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Adresas = dre.From<string>("adresas");
				t.FkVairavimoMokykla = dre.From<string>("fk_VAIRAVIMO_MOKYKLA");
			});

		return result;
	}	
}
