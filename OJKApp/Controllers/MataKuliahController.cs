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

namespace OJKApp.Controllers
{
    public class MataKuliahController : Controller
    {
        private readonly IConfiguration _configuration;
        public MataKuliahController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: MataKuliah
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("MKViewAll", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
        }

        // GET: MataKuliah/AddOrEdit/5
        public IActionResult AddOrEdit(int? id)
        {
            MataKuliahViewModel mataKuliahViewModel = new MataKuliahViewModel();
            if (id > 0)
            {
                mataKuliahViewModel = FetchMKById(id);
            }
            return View(mataKuliahViewModel);
        }

        // POST: MataKuliah/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("id,kodeMatkul,namaMatkul,sks")] MataKuliahViewModel mataKuliahViewModel)
        {

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("MKAddOrEdit", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("id", mataKuliahViewModel.id);
                    sqlCmd.Parameters.AddWithValue("kodeMatkul", mataKuliahViewModel.kodeMatkul);
                    sqlCmd.Parameters.AddWithValue("namaMatkul", mataKuliahViewModel.namaMatkul);
                    sqlCmd.Parameters.AddWithValue("sks", mataKuliahViewModel.sks);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mataKuliahViewModel);
        }

        // GET: MataKuliah/Delete/5
        public IActionResult Delete(int? id)
        {
            MataKuliahViewModel mataKuliahViewModel = FetchMKById(id);
            return View(mataKuliahViewModel);
        }

        // POST: MataKuliah/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("MKDeleteByID", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public MataKuliahViewModel FetchMKById(int? id)
        {
            MataKuliahViewModel mataKuliahViewModel = new MataKuliahViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("MKViewByID", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("id", id);
                sqlDa.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    mataKuliahViewModel.id = Convert.ToInt32(dtbl.Rows[0]["id"].ToString());
                    mataKuliahViewModel.kodeMatkul = dtbl.Rows[0]["kodeMatkul"].ToString();
                    mataKuliahViewModel.namaMatkul = dtbl.Rows[0]["namaMatkul"].ToString();
                    mataKuliahViewModel.sks = Convert.ToInt32(dtbl.Rows[0]["sks"].ToString());
                }
                return mataKuliahViewModel;
            }
        }
    }
}
