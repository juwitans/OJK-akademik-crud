CREATE PROCEDURE [dbo].[MahasiswaAddOrEdit]
	@id INT,
	@nim VARCHAR(9),
	@namaMhs VARCHAR(25),
	@tglLahir DATE,
	@alamat VARCHAR(50),
	@jenisKelamin VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

	IF @id=0
	BEGIN
		INSERT INTO mahasiswa(nim,nama_mhs,tgl_lahir,alamat,jenis_kelamin)
		VALUES (@nim,@namaMhs,@tglLahir,@alamat,@jenisKelamin)
	END
	ELSE
	BEGIN
		UPDATE mahasiswa
		SET
			nim=@nim,
			nama_mhs=@namaMhs,
			tgl_lahir=@tglLahir,
			alamat=@alamat,
			jenis_kelamin=@jenisKelamin
		WHERE id = @id
	END
END