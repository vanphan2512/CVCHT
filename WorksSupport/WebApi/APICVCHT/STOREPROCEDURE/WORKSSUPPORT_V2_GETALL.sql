CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_V2_GETALL
(
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
  OPEN V_OUT FOR
  SELECT
    SUPPORT.WORKSSUPPORTID,
    SUPPORT.WORKSSUPPORTTYPEID,
    SUPPORT.WORKSSUPPORTNAME,
    SUPPORT.CONTENT,
    SUPPORT.WORKSSUPPORTSTATUSID,
    STATUS.WORKSSUPPORTSTATUSNAME,
    SUPPORT.EXPECTEDCOMPLETEDDATE,
    SUPPORT.COMPLETEDDATE,
    SUPPORT.CURRENTPROGRESS,
    SUPPORT.CREATEDUSER,
    SUPPORT.CREATEDDATE,
    SUPPORT.UPDATEDUSER,
    SUPPORT.UPDATEDDATE,
    SUPPORT.ISDELETED,
    SUPPORT.DELETEDUSER,
    SUPPORT.DELETEDDATE,
    SUPPORT.LASTCOMMENTTIME,
    SUPPORT.LASTACTIONTIME,
    SUPPORT.LASTCOMMENTID,
    SUPPORT.ATTACHMENTFILECOUNT,
    SUPPORT.WORKSSUPPORTGROUPID,
    SUPPORT.WORKSSUPPORTPRIORITYID,
    SUPPORT.WORKSSUPPORTQUALITYID,
    SUPPORT.SOLUTIONCONTENT,
    STATUS.WORKSSUPPORTSTATUSNAME
    
  FROM
    ERP.EO_WORKSSUPPORT SUPPORT
    JOIN EO_WORKSSUPPORTSTATUS STATUS ON STATUS.WORKSSUPPORTSTATUSID = SUPPORT.WORKSSUPPORTSTATUSID
  WHERE
    SUPPORT.ISDELETED = 0

  ORDER BY
    SUPPORT.CREATEDDATE
  ;
END;
/