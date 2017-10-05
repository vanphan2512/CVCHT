CREATE OR REPLACE PROCEDURE ERP."EO_WORKSSUPPORT_V2_ATT_ADD" 
/*
	Description	:	Thêm thông tin Danh sách file dính kèm c?a công viec can ho tro
*/
(
	v_WorksSupportID IN EO_WorksSupport_Attachment.WorksSupportID%TYPE,
	v_FilePath IN EO_WorksSupport_Attachment.FilePath%TYPE,
	v_FileName IN EO_WorksSupport_Attachment.FileName%TYPE,
	v_CreatedUser IN EO_WorksSupport_Attachment.CreatedUser%TYPE,
  v_FILEID IN EO_WorksSupport_Attachment.FILEID%TYPE DEFAULT NULL
)
AS
BEGIN

	INSERT
	INTO EO_WorksSupport_Attachment
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