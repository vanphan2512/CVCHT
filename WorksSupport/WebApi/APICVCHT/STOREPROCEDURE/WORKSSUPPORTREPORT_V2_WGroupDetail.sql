CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTREPORT_V2_WGroupDetail
/*
	CREATED BY	:	NGUYEN VAN HUAN
	      DATE  :	13/09/2017
	DESCRIPTION	:	L?Y chi ti?t DANH S�CH nh�m C�NG VI?C 
*/
( 
   V_OUT OUT SYS_REFCURSOR,
   V_WORKSSUPPORTPROJECTID IN EO_WORKSSUPPORTGROUP.WORKSSUPPORTPROJECTID%TYPE,   
   V_WORKSSUPPORTGROUPID IN EO_WORKSSUPPORTGROUP.WORKSSUPPORTGROUPID%TYPE,
   V_FROMDATE IN EO_WORKSSUPPORT.CREATEDDATE%TYPE,
   V_TODATE IN EO_WORKSSUPPORT.CREATEDDATE%TYPE,
   V_WORKSSUPPORTSTATUSID IN EO_WORKSSUPPORTSTATUS.WORKSSUPPORTSTATUSID%TYPE       
  )
AS 
BEGIN
 OPEN V_OUT FOR
  SELECT  W_GROUP.WORKSSUPPORTGROUPID
           ,W_GROUP.WORKSSUPPORTGROUPNAME
           ,COUNT(1) AS NUMBEROFWORK 
      FROM     EO_WORKSSUPPORTGROUP W_GROUP
 LEFT JOIN EO_WORKSSUPPORT W_WORK
        ON W_WORK.WORKSSUPPORTGROUPID = W_GROUP.WORKSSUPPORTGROUPID
           AND W_WORK.ISDELETED = 0           
           AND W_WORK.WORKSSUPPORTSTATUSID = V_WORKSSUPPORTSTATUSID
 LEFT JOIN EO_WORKSSUPPORTSTATUS W_STATUS
        ON W_STATUS.WORKSSUPPORTSTATUSID = W_WORK.WORKSSUPPORTSTATUSID        
     WHERE W_GROUP.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
           AND W_GROUP.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID
           AND W_GROUP.ISDELETED = 0
           AND (V_FROMDATE IS NULL OR TRUNC(V_FROMDATE) <= TRUNC(W_WORK.CREATEDDATE))
           AND (V_TODATE IS NULL OR TRUNC(V_TODATE) >= TRUNC(W_WORK.CREATEDDATE))
           AND W_WORK.WORKSSUPPORTID IS NOT NULL
 GROUP BY  W_GROUP.WORKSSUPPORTGROUPID, W_GROUP.WORKSSUPPORTGROUPNAME ; 
                          
END;
/