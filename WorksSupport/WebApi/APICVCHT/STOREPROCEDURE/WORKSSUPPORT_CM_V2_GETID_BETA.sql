CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_CM_V2_GETID_BETA
(
  v_Out OUT SYS_REFCURSOR,
  v_WORKSSUPPORTID IN EO_WORKSSUPPORT_COMMENT.WORKSSUPPORTID%TYPE,
  v_PageIdx NUMBER,
  V_PageSize NUMBER
)
AS
BEGIN
  OPEN v_Out FOR
  SELECT
      DT_PG.*
  FROM (
    SELECT
        ROWNUM AS RN,
        EO_WSC.*
    FROM (
      SELECT
        EO_WORKSSUPPORT_COMMENT.COMMENTID,
        EO_WORKSSUPPORT_COMMENT.WORKSSUPPORTID,
        EO_WORKSSUPPORT_COMMENT.COMMENTDATE,
        EO_WORKSSUPPORT_COMMENT.COMMENTCONTENT,
        EO_WORKSSUPPORT_COMMENT.COMMENTUSER,
        EO_WORKSSUPPORT_COMMENT.ORDERINDEX,
        EO_WORKSSUPPORT_COMMENT.ISACTIVE,
        EO_WORKSSUPPORT_COMMENT.ISSYSTEM,
        EO_WORKSSUPPORT_COMMENT.CREATEDUSER,
        EO_WORKSSUPPORT_COMMENT.CREATEDDATE,
        EO_WORKSSUPPORT_COMMENT.UPDATEDUSER,
        EO_WORKSSUPPORT_COMMENT.UPDATEDDATE,
        EO_WORKSSUPPORT_COMMENT.ISDELETED,
        EO_WORKSSUPPORT_COMMENT.DELETEDUSER,
        EO_WORKSSUPPORT_COMMENT.DELETEDDATE,
        
        SYS_USER.FULLNAME,
        SYS_POSITION.POSITIONNAME,
        SYS_DEPARTMENT.DEPARTMENTNAME,
        SYS_USER.DEFAULTPICTUREURL
    
      FROM
        EO_WORKSSUPPORT_COMMENT
        JOIN SYS_USER
          ON LOWER(EO_WORKSSUPPORT_COMMENT.CREATEDUSER) = LOWER(SYS_USER.USERNAME)
        JOIN SYS_DEPARTMENT 
          ON SYS_USER.DEPARTMENTID = SYS_DEPARTMENT.DEPARTMENTID
       JOIN SYS_POSITION
          ON SYS_USER.POSITIONID = SYS_POSITION.POSITIONID
      WHERE
        EO_WORKSSUPPORT_COMMENT.ISDELETED = 0
        AND EO_WORKSSUPPORT_COMMENT.WORKSSUPPORTID = v_WORKSSUPPORTID
        ORDER BY EO_WORKSSUPPORT_COMMENT.COMMENTDATE DESC
    ) EO_WSC
  ) DT_PG
  WHERE
    DT_PG.RN > (V_PAGEIDX * V_PAGESIZE)
    AND DT_PG.RN <= (V_PAGEIDX * V_PAGESIZE + V_PAGESIZE)
  ;
END;
/