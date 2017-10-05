CREATE OR REPLACE PROCEDURE ERP.EO_WORKSSUPPORT_SOLUTION_ADD
/*
	Description	:	Th�m th�ng tin Danh s�ch file d�nh k�m c?a c�ng viec can ho tro
*/
(
	v_WorksSupportID IN EO_WORKSSUPPORT_SOLUTIONATM.WorksSupportID%TYPE,
	v_FilePath IN EO_WORKSSUPPORT_SOLUTIONATM.FilePath%TYPE,
	v_FileName IN EO_WORKSSUPPORT_SOLUTIONATM.FileName%TYPE,
	v_CreatedUser IN EO_WORKSSUPPORT_SOLUTIONATM.CreatedUser%TYPE,
  v_FILEID IN EO_WORKSSUPPORT_SOLUTIONATM.FILEID%TYPE DEFAULT NULL
)
AS
BEGIN

	INSERT
	INTO EO_WORKSSUPPORT_SOLUTIONATM
	(
		WorksSupportID,
		FilePath,
		FileName,
		CreatedUser,
		CreatedDate,
    FILEID
	)
	VALUES
	(
		v_WorksSupportID,
		v_FilePath,
		v_FileName,
		v_CreatedUser,
		SYSDATE,
    v_FILEID
	);

END;
/