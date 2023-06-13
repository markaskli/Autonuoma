namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Mokykla;


/// <summary>
/// Database operations related to 'Vairavimo Mokykla' entity.
/// </summary>
public class MokyklaRepo
{
	public static List<MokyklaL> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}vairavimo_mokyklos` ORDER BY imones_kodas ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<MokyklaL>(drc, (dre, t) => {
				t.Kodas = dre.From<string>("imones_kodas");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Vadovas = dre.From<string>("vadovas");
			});

		return result;
	}

	public static MokyklaCE Find(string imonesKodas)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}vairavimo_mokyklos` WHERE imones_kodas=?imonesKodas";

		var drc =
			Sql.Query(query, args => {
				args.Add("?imoneskodas", imonesKodas);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<MokyklaCE>(drc, (dre, t) => {
					t.Mokykla.Kodas = dre.From<string>("imones_kodas");
					t.Mokykla.Pavadinimas = dre.From<string>("pavadinimas");
					t.Mokykla.Vadovas = dre.From<string>("vadovas");
					t.Mokykla.telefono_nr = dre.From<string>("telefono_nr");
					t.Mokykla.elpastas = dre.From<string>("elpastas");
					t.Mokykla.darbuotoju_skaicius = dre.From<int>("darbuotoju_skaicius");
				});

			return result;
		}

		return null;
	}

	public static void Insert(MokyklaCE mokykla)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}vairavimo_mokyklos`
			(
				imones_kodas,
				pavadinimas,
				vadovas,
				telefono_nr,
				elpastas,
				darbuotoju_skaicius
			)
			VALUES(
				?imnkod,
				?pavadinimas,
				?vadovas,
				?tel,
				?elpastas,
				?darbuotojusk
			)";

		Sql.Insert(query, args => {
			args.Add("?imnkod", mokykla.Mokykla.Kodas);
			args.Add("?pavadinimas", mokykla.Mokykla.Pavadinimas);
			args.Add("?vadovas", mokykla.Mokykla.Vadovas);
			args.Add("?tel", mokykla.Mokykla.telefono_nr);
			args.Add("?elpastas", mokykla.Mokykla.elpastas);
			args.Add("?darbuotojusk", mokykla.Mokykla.darbuotoju_skaicius);
		});
	}

	public static void Update(MokyklaCE mokykla)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}vairavimo_mokyklos`
			SET
				pavadinimas=?pavadinimas,
				vadovas=?vadovas,
				telefono_nr=?telefononr,
				elpastas=?elpastas,
				darbuotoju_skaicius=?darbuotojusk
			WHERE
				imones_kodas=?imnkodas";

		Sql.Update(query, args => {
			args.Add("?imnkodas", mokykla.Mokykla.Kodas);
			args.Add("?pavadinimas", mokykla.Mokykla.Pavadinimas);
			args.Add("?vadovas", mokykla.Mokykla.Vadovas);
			args.Add("?telefononr", mokykla.Mokykla.telefono_nr);
			args.Add("?elpastas", mokykla.Mokykla.elpastas);
			args.Add("?darbuotojusk", mokykla.Mokykla.darbuotoju_skaicius);
		});
	}

	public static void Delete(string id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}vairavimo_mokyklos` WHERE imones_kodas=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}
