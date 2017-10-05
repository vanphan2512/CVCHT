CREATE OR REPLACE PROCEDURE ERP.EWORKSSUPPORT_CM_V2_ADD_BETA
/*
	Author	    :	Byrs
	Date		    :	Apr 19, 2013
	Description	:	Add new comment for work support
*/
(
  V_OUT OUT NUMBER,
  V_WORKSSUPPORTID IN eo_workssupport_comment.workssupportid%TYPE,
  V_COMMENTCONTENT IN eo_workssupport_comment.commentcontent%TYPE,
  V_COMMENTUSER IN eo_workssupport_comment.commentuser%TYPE,
  V_CREATEDUSER IN eo_workssupport_comment.createduser%TYPE
)
AS
BEGIN
  INSERT INTO erp.eo_workssupport_comment
  (
    workssupportid,
    commentcontent,
    commentuser,
    createduser,
    commentdate
  )
  VALUES
  (
    V_WORKSSUPPORTID,
    V_COMMENTCONTENT,
    V_COMMENTUSER,
    V_CREATEDUSER,
    SYSDATE
  );

  SELECT eo_workssupport_comment_seq.CURRVAL INTO V_OUT FROM DUAL;
  	UPDATE EO_WORKSSUPPORT
  	SET
  	  LASTCOMMENTID = V_OUT,
      LASTACTIONTIME = SYSDATE 
  	WHERE
  		WORKSSUPPORTID = V_WORKSSUPPORTID
    ;
END;
/