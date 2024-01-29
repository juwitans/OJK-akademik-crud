CREATE PROCEDURE [dbo].[MahasiswaViewAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT id AS id, nim AS nim, nama_mhs AS namaMhs, tgl_lahir AS tglLahir, alamat AS alamat, jenis_kelamin AS jenisKelamin
	FROM mahasiswa
END