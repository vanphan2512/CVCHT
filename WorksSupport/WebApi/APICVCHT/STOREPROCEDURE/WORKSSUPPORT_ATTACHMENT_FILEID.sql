create or replace PROCEDURE     WORKSSUPPORT_ATTACHMENT_FILEID
/*
	CREATED BY	:	L��NG TRUNG NH�N
	DATE		:	19/07/2017
	DESCRIPTION	:	N?P TH�NG TIN DANH S�CH FILE D�NH K�M C?A C�NG VI?C C?N H? TR? 
*/
(	
	V_FILEID IN EO_WORKSSUPPORT_ATTACHMENT.FILEID%TYPE,	
	V_OUT OUT SYS_REFCURSOR
) 
AS
BEGIN
    OPEN V_OUT FOR
	SELECT
        ATTACHMENTID,
		WORKSSUPPORTID,
		FILEPATH,
		FILENAME,
        FILEID
	FROM 
        EO_WORKSSUPPORT_ATTACHMENT
	WHERE
		FILEID = V_FILEID
  AND ISDELETED = 0
;
END;