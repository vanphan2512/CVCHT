create or replace PROCEDURE     WORKSSUPORTPROJECT_V2_PERMISS
(
  V_OUT OUT SYS_REFCURSOR,
  V_USERNAME IN ERP.EO_WORKSSUPPORTPROJECT_MEMBER.MEMBERUSERNAME%TYPE,
  V_KEYWORDS NVARCHAR2 DEFAULT NULL,
  V_PAGEINDEX NUMBER DEFAULT -1,
  V_PAGESIZE NUMBER DEFAULT 500
)
AS
BEGIN
  IF(V_USERNAME = 'administrator') THEN 
    OPEN V_OUT FOR
     SELECT    
        PROJECT.WORKSSUPPORTPROJECTID,
        PROJECT.WORKSSUPPORTPROJECTNAME,
        PROJECT.WORKSSUPPORTTYPEID,
        PROJECT.ISACTIVE,
        PROJECT.ISSYSTEM,
        PROJECT.ICONURL,
        (SELECT COUNT(1) 
          FROM EO_WORKSSUPPORTGROUP EW 
            WHERE EW.WORKSSUPPORTPROJECTID = PROJECT.WORKSSUPPORTPROJECTID
              AND EW.ISDELETED =0)AS NUMBEROFWORKGROUP,
        (SELECT COUNT(1)
          FROM EO_WORKSSUPPORTPROJECT_MEMBER EWM
          WHERE EWM.WORKSSUPPORTPROJECTID = PROJECT.WORKSSUPPORTPROJECTID
            AND EWM.ISDELETED =0) AS NUMBEROFMEMBER,
            1 AS ISCANADD,
            1 AS ISCANEDIT,
            1 AS ISCANDELETE,
            1 AS ISCANVIEW,
            1 AS ISCANVIEWREPORT
      FROM
        ERP.EO_WORKSSUPPORTPROJECT PROJECT, EO_WORKSSUPPORTTYPE EW
      WHERE 
            PROJECT.WORKSSUPPORTTYPEID = EW.WORKSSUPPORTTYPEID  AND  PROJECT.ISDELETED = 0 
            AND (V_KEYWORDS IS NULL 
             OR UPPER( PROJECT.WORKSSUPPORTPROJECTNAME)  LIKE '%' ||UPPER ( V_KEYWORDS )|| '%') 

       ORDER BY
        PROJECT.ISACTIVE DESC,
        PROJECT.WORKSSUPPORTPROJECTNAME ASC;
  ELSE 
    OPEN V_OUT FOR
    SELECT    
        PROJECT.WORKSSUPPORTPROJECTID,
        PROJECT.WORKSSUPPORTPROJECTNAME,
        PROJECT.WORKSSUPPORTTYPEID,
        PROJECT.ISACTIVE,
        PROJECT.ISSYSTEM,
        PROJECT.ICONURL,
        (SELECT COUNT(1) 
          FROM EO_WORKSSUPPORTGROUP EW 
            WHERE EW.WORKSSUPPORTPROJECTID = PROJECT.WORKSSUPPORTPROJECTID
              AND EW.ISDELETED =0)AS NUMBEROFWORKGROUP,
        (SELECT COUNT(1)
          FROM EO_WORKSSUPPORTPROJECT_MEMBER EWM
          WHERE EWM.WORKSSUPPORTPROJECTID = PROJECT.WORKSSUPPORTPROJECTID
            AND EWM.ISDELETED =0) AS NUMBEROFMEMBER,
            EWP.ISCANADDPROJECT AS ISCANADD,
            EWP.ISCANEDITPROJECT AS ISCANEDIT,
            EWP.ISCANDELETEPROJECT AS ISCANDELETE,
            EWP.iscanviewproject AS ISCANVIEW,
            EWP.ISCANVIEWPROJECTREPORT AS ISCANVIEWREPORT
      FROM
        ERP.EO_WORKSSUPPORTPROJECT PROJECT, EO_WORKSSUPPORTTYPE EW, EO_WORKSSUPPORTPROJECT_PERMIS EWP
      WHERE 
            PROJECT.WORKSSUPPORTTYPEID = EW.WORKSSUPPORTTYPEID AND
            EW.WORKSSUPPORTTYPEID = EWP.WORKSSUPPORTTYPEID AND 
            EWP.USERNAME = V_USERNAME  AND PROJECT.ISDELETED = 0  
            AND (V_KEYWORDS IS NULL 
               OR UPPER( PROJECT.WORKSSUPPORTPROJECTNAME)  LIKE '%' ||UPPER ( V_KEYWORDS )|| '%')
       ORDER BY
        PROJECT.ISACTIVE DESC;
  END IF;
END;
