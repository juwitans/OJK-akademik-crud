using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OJKApp.Data;
using OJKApp.Models;
using static OJKApp.Models.MahasiswaViewModel;

namespace OJKApp.Controllers
{
    public class MahasiswaController : Controller
    {
        private readonly IConfiguration _configuration;
        public MahasiswaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: Mahasiswa
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("MahasiswaViewAll", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
        }

        // GET: Mahasiswa/Edit/5
        public IActionResult AddOrEdit(int? id)
        {
            MahasiswaViewModel mahasiswaViewModel = new MahasiswaViewModel();
            if (id > 0)
            {
                mahasiswaViewModel = FetchMahasiswaById(id);
            }
            return View(mahasiswaViewModel);
        }

        // POST: Mahasiswa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("id,nim,namaMhs,tglLahir,alamat,jenisKelamin")] MahasiswaViewModel mahasiswaViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("MahasiswaAddOrEdit", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("id", mahasiswaViewModel.id);
                    sqlCmd.Parameters.AddWithValue("nim", mahasiswaViewModel.nim);
                    sqlCmd.Parameters.AddWithValue("namaMhs", mahasiswaViewModel.namaMhs);
                    sqlCmd.Parameters.AddWithValue("tglLahir", mahasiswaViewModel.tglLahir);
                    sqlCmd.Parameters.AddWithValue("alamat", mahasiswaViewModel.alamat);
                    sqlCmd.Parameters.AddWithValue("jenisKelamin", mahasiswaViewModel.jenisKelamin);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mahasiswaViewModel);
        }

        // GET: Mahasiswa/Delete/5
        public IActionResult Delete(int? id)
        {
            MahasiswaViewModel mahasiswaViewModel = FetchMahasiswaById(id);
            return View(mahasiswaViewModel);
        }

        // POST: Mahasiswa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("MahasiswaDeleteByID", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public MahasiswaViewModel FetchMahasiswaById(int? id)
        {
            MahasiswaViewModel mahasiswaViewModel = new MahasiswaViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("MahasiswaViewByID", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("id", id);
                sqlDa.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    mahasiswaViewModel.id = Convert.ToInt32(dtbl.Rows[0]["id"].ToString());
                    mahasiswaViewModel.nim = dtbl.Rows[0]["nim"].ToString();
                    mahasiswaViewModel.namaMhs = dtbl.Rows[0]["namaMhs"].ToString();
                    mahasiswaViewModel.tglLahir = DateOnly.Parse(dtbl.Rows[0]["tglLahir"].ToString());
                    mahasiswaViewModel.alamat = dtbl.Rows[0]["alamat"].ToString();
                    mahasiswaViewModel.jenisKelamin = EnumFromString<JenisKelaminEnum>(dtbl.Rows[0]["jenisKelamin"].ToString());
                }
                return mahasiswaViewModel;
            }
        }

        public static T EnumFromString<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
