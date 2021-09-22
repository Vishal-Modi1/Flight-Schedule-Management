ALTER PROCEDURE [dbo].[GetUserList]  
(       
    @SearchValue NVARCHAR(50) = NULL,  
    @PageNo INT = 1,  
    @PageSize INT = 10,  
    @SortColumn NVARCHAR(20) = 'FirstName',  
    @SortOrder NVARCHAR(20) = 'ASC'  
)  
AS BEGIN  
    SET NOCOUNT ON;  
  
    SET @SearchValue = LTRIM(RTRIM(@SearchValue))  
  
    ; WITH CTE_Results AS   
    (  
        SELECT U.*,UR.Name from Users U
		LEFT JOIN  UserRoles UR on UR.Id=U.RoleId
  
        WHERE @SearchValue= '' OR  (   
              FirstName LIKE '%' + @SearchValue + '%' OR
			  LastName LIKE '%' + @SearchValue + '%' OR
			  Email LIKE '%' + @SearchValue + '%' OR
			  UR.Name LIKE '%' + @SearchValue + '%'
            )  
  
            ORDER BY  
            CASE WHEN (@SortColumn = 'FirstName' AND @SortOrder='ASC')  
                        THEN FirstName  
            END ASC,  
            CASE WHEN (@SortColumn = 'FirstName' AND @SortOrder='DESC')  
                        THEN FirstName  
            END DESC, 
			CASE WHEN (@SortColumn = 'LastName' AND @SortOrder='ASC')  
                        THEN LastName  
            END ASC,  
            CASE WHEN (@SortColumn = 'LastName' AND @SortOrder='DESC')  
                        THEN LastName  
            END DESC, 
			CASE WHEN (@SortColumn = 'Email' AND @SortOrder='ASC')  
                        THEN Email  
            END ASC,  
            CASE WHEN (@SortColumn = 'Email' AND @SortOrder='DESC')  
                        THEN Email  
            END DESC, 
            CASE WHEN (@SortColumn = 'UserRole' AND @SortOrder='ASC')  
                        THEN UR.Name  
            END ASC,  
            CASE WHEN (@SortColumn = 'UserRole' AND @SortOrder='DESC')  
                        THEN UR.Name  
            END DESC,
			CASE WHEN (@SortColumn = 'IsIntructor' AND @SortOrder='ASC')  
                        THEN U.IsInstructor 
            END ASC,  
            CASE WHEN (@SortColumn = 'IsIntructor' AND @SortOrder='DESC')  
                        THEN U.IsInstructor  
            END DESC, 
			CASE WHEN (@SortColumn = 'IsActive' AND @SortOrder='ASC')  
                        THEN U.IsActive  
            END ASC,  
            CASE WHEN (@SortColumn = 'IsActive' AND @SortOrder='DESC')  
                        THEN U.IsActive 
            END DESC 
            OFFSET @PageSize * (@PageNo - 1) ROWS  
            FETCH NEXT @PageSize ROWS ONLY  
    ),  
    CTE_TotalRows AS   
    (  
        select count(U.ID) as TotalRecords from Users U
		LEFT JOIN  UserRoles UR on UR.Id=U.RoleId
        WHERE @SearchValue= '' OR  (   
              FirstName LIKE '%' + @SearchValue + '%' OR
			  LastName LIKE '%' + @SearchValue + '%' OR
			  Email LIKE '%' + @SearchValue + '%' OR
			  UR.Name LIKE '%' + @SearchValue + '%'
            )   
    )  
    Select TotalRecords, U.Id, U.FirstName, U.LastName,U.Email,ISNULL(U.IsInstructor, 0 ) AS IsInstructor,ISNULL(U.IsActive, 0 ) AS IsActive,Ur.Name as UserRole from Users U
	LEFT JOIN  UserRoles UR on UR.Id=U.RoleId
	, CTE_TotalRows   
    WHERE EXISTS (SELECT 1 FROM CTE_Results WHERE CTE_Results.ID = U.ID)  
   
END