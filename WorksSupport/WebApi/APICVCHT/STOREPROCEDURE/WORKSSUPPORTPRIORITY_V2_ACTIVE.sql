create or replace PROCEDURE  WORKSSUPPORTPRIORITY_V2_ACTIVE
  /*
	Created by	:	Nguyen thi kim ngan 
	Date		:	29.05.2014
	Description	:	N?p thông tin do uu tien
*/
(
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
  OPEN V_OUT FOR
  SELECT
    workssupportpriorityid,
    workssupportpriorityname
  FROM
    EO_WORKSSUPPORTPRIORITY 
  WHERE
     EO_WORKSSUPPORTPRIORITY.ISACTIVE = 1 AND
     EO_WORKSSUPPORTPRIORITY.ISDELETED = 0
  ORDER BY
    EO_WORKSSUPPORTPRIORITY.ORDERINDEX ASC;
END;