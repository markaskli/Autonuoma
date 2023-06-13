namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Paslauga;


/// <summary>
/// Database operations related to 'Paslauga' entity.
/// </summary>
public class PaslaugaRepo
{
	public static List<PaslaugaL> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}paslaugos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<PaslaugaL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Tipas = dre.From<string>("tipas");
			});

		return result;
	}

	public static PaslaugaCE Find(int Id)
	{
		var query = 
			$@"SELECT * FROM `{Config.TblPrefix}paslaugos` WHERE id=?Id";

		var drc = 
			Sql.Query(query, args => {
				args.Add("?Id", Id);
			});

		var result = 
			Sql.MapOne<PaslaugaCE>(drc, (dre, t) => {
				
				t.Paslauga.Pavadinimas = dre.From<string>("pavadinimas");
				t.Paslauga.Tipas = dre.From<string>("tipas");
				t.Paslauga.Aprasymas = dre.From<string>("aprasymas");
				t.Paslauga.valanduSkaicius = dre.From<int>("akademiniu_valandu_skaicius");
				t.Paslauga.Id = dre.From<int>("id");
			});

		return result;
	}

	public static void Update(PaslaugaCE paslauga)
	{
		var query = 
			$@"UPDATE `{Config.TblPrefix}paslaugos`
			SET 
				pavadinimas=?pavadinimas, 
				tipas=?tipas, 
				aprasymas=?aprasymas, 
				akademiniu_valandu_skaicius=?valanduSkaicius 
			WHERE 
				id=?Id";

		Sql.Update(query, args => {
			args.Add("?pavadinimas", paslauga.Paslauga.Pavadinimas);
			args.Add("?tipas", paslauga.Paslauga.Tipas);
			args.Add("?aprasymas", paslauga.Paslauga.Aprasymas);
			args.Add("?valanduSkaicius", paslauga.Paslauga.valanduSkaicius);
			args.Add("?Id", paslauga.Paslauga.Id);
		});
	}

	public static void Insert(PaslaugaCE paslauga)
	{
		string query = 
			$@"INSERT INTO `{Config.TblPrefix}paslaugos`
			(
				pavadinimas,
				tipas,
				aprasymas,
				akademiniu_valandu_skaicius
			)
			VALUES(
				?pavadinimas,
				?tipas,
				?aprasymas,
				?akademiniu_valandu_skaicius
			)";

		var id =
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", paslauga.Paslauga.Pavadinimas);
				args.Add("?tipas", paslauga.Paslauga.Tipas);
				args.Add("?aprasymas", paslauga.Paslauga.Aprasymas);
				args.Add("?akademiniu_valandu_skaicius", paslauga.Paslauga.valanduSkaicius);
			});

	}

	public static void Delete(int Id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}paslaugos` WHERE id=?Id";
		Sql.Delete(query, args => {
			args.Add("?Id", Id);
		});
	}
}
