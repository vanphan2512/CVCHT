create or replace PROCEDURE EO_WORKSSUPPORT_DETAIL
/*
	CREATED BY	:	NGUYEN VAN HUAN
	DATE		:	07/03/2013
	DESCRIPTION	:	N?P TH�NG TIN C�NG VI?C C?N H? TR? 
*/ (
    V_WORKSSUPPORTID   IN EO_WORKSSUPPORT.WORKSSUPPORTID%TYPE,
    V_OUT              OUT SYS_REFCURSOR
)
    AS
BEGIN
    OPEN V_OUT FOR
       SELECT
            EO_WORKSSUPPORT.WORKSSUPPORTID,
            EO_WORKSSUPPORT.WORKSSUPPORTTYPEID,
            EO_WORKSSUPPORT.WORKSSUPPORTNAME,
            EO_WORKSSUPPORT.WORKSSUPPORTQUALITYID,
            EO_WORKSSUPPORT.WORKSSUPPORTPRIORITYID,
            EO_WORKSSUPPORT.WORKSSUPPORTGROUPID,
            EO_WORKSSUPPORT.WORKSSUPPORTQUALITYNOTE,            
            EO_WORKSSUPPORT.SOLUTIONCONTENT,
            EO_WORKSSUPPORT.CONTENT,
            EO_WORKSSUPPORT.WORKSSUPPORTSTATUSID,
            EO_WORKSSUPPORT.EXPECTEDCOMPLETEDDATE,
            EO_WORKSSUPPORT.COMPLETEDDATE,
            EO_WORKSSUPPORT.CURRENTPROGRESS,            
            EO_WORKSSUPPORT.CREATEDDATE,
            EO_WORKSSUPPORT.SOLUTIONCONTENT,
            (
                SELECT
                    FULLNAME
                FROM
                    SYS_USER
                WHERE 
                    SYS_USER.USERNAME = EO_WORKSSUPPORT.UPDATESOLUTIONUSER
            ) AS UPDATESOLUTIONUSER,
            EO_WORKSSUPPORT.SOLUTIONCONTENT,
            (
                SELECT
                    DEFAULTPICTUREURL
                FROM
                    SYS_USER
                WHERE 
                    SYS_USER.USERNAME = EO_WORKSSUPPORT.UPDATESOLUTIONUSER
            ) AS UPDATESOLUTIONUSERURL,
            EO_WORKSSUPPORT.UPDATESOLUTIONDATE,

            EO_WORKSSUPPORT.ISDELETED,
            EO_WORKSSUPPORT.DELETEDUSER,
            EO_WORKSSUPPORT.DELETEDDATE,
            EO_WORKSSUPPORT.ATTACHMENTFILECOUNT,
            ST.WORKSSUPPORTSTATUSNAME,
            ST.COLORCODE AS STATUSCOLOR,
            REORITY.WORKSSUPPORTPRIORITYNAME,
            REORITY.COLORCODE AS REORITYCOLORCODE,
            U.FULLNAME,
            ( -- GET TRANG THAI CONG VIEC CO PHAI LA TRANG THAI HOAN THANH
                SELECT
                    ISFINISHSTEP
                FROM
                    EO_WORKSSUPPORTTYPE_WORKFLOW
                WHERE
                    EO_WORKSSUPPORTTYPE_WORKFLOW.WORKSSUPPORTSTEPID = EO_WORKSSUPPORT.CURRENTWORKSSUPPORTSTEPID
            ) AS ISFINISHSTEP,
            (   
            SELECT
                WORKFLOW.PROCESSUSER
            FROM 
                EO_WORKSSUPPORT_WORKFLOW WORKFLOW

                WHERE
                    WORKFLOW.WORKSSUPPORTSTEPID = EO_WORKSSUPPORT.CURRENTWORKSSUPPORTSTEPID
                AND 
                    WORKFLOW.WORKSSUPPORTID = EO_WORKSSUPPORT.WORKSSUPPORTID
                AND
                    EO_WORKSSUPPORT.WORKSSUPPORTID = EO_WORKSSUPPORT.WORKSSUPPORTID
                AND
                    WORKFLOW.ISPROCESS = 0
                ) AS PROCESSUSER
        FROM
            EO_WORKSSUPPORT EO_WORKSSUPPORT
        INNER JOIN -- Get ten trang thai
            EO_WORKSSUPPORTSTATUS ST
        ON 
            EO_WORKSSUPPORT.WORKSSUPPORTSTATUSID = ST.WORKSSUPPORTSTATUSID
        INNER JOIN -- Get ten do uu tien
            EO_WORKSSUPPORTPRIORITY REORITY
        ON 
            EO_WORKSSUPPORT.WORKSSUPPORTPRIORITYID = REORITY.WORKSSUPPORTPRIORITYID            

        INNER JOIN -- Get FullName
            SYS_USER U
        ON 
            EO_WORKSSUPPORT.CREATEDUSER = U.USERNAME     
        WHERE
            EO_WORKSSUPPORT.WORKSSUPPORTID = V_WORKSSUPPORTID
        AND
            EO_WORKSSUPPORT.ISDELETED = 0;

END;