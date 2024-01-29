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
    public class DosenController : Controller
    {
        private readonly IConfiguration _configuration;
        public DosenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: Dosen
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("DosenViewAll", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
        }

        // GET: Dosen/Edit/5
        public IActionResult AddOrEdit(int? id)
        {
            DosenViewModel dosen = new DosenViewModel();
            if (id > 0)
                dosen = FetchDosenById(id);
            return View(dosen);
        }

        // POST: Dosen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("id,nip,namaDosen")] DosenViewModel dosenViewModel)
        {
            
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("DosenAddOrEdit", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("id", dosenViewModel.id);
                    sqlCmd.Parameters.AddWithValue("nip", dosenViewModel.nip);
                    sqlCmd.Parameters.AddWithValue("namaDosen", dosenViewModel.namaDosen);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dosenViewModel);
        }

        // GET: Dosen/Delete/5
        public IActionResult Delete(int? id)
        {
            DosenViewModel dosenViewModel = FetchDosenById(id);
            return View(dosenViewModel);
        }

        // POST: Dosen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("DosenDeleteByID", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public DosenViewModel FetchDosenById(int? id)
        {
            DosenViewModel dosenViewModel = new DosenViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("DosenViewByID", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("id", id);
                sqlDa.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    dosenViewModel.id = Convert.ToInt32(dtbl.Rows[0]["id"].ToString());
                    dosenViewModel.nip = dtbl.Rows[0]["nip"].ToString();
                    dosenViewModel.namaDosen = dtbl.Rows[0]["namaDosen"].ToString();
                }
                return dosenViewModel;
            }
        }
    }
}
