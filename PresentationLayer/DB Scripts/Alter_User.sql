
ALTER TABLE Users 
add IsSendTextMessage bit not null default(0)
Go

ALTER TABLE Users
ALTER COLUMN IsSendEmailInvite bit not null

ALTER TABLE Users
ALTER COLUMN IsActive bit not null

ALTER TABLE Users
ALTER COLUMN IsDeleted bit not null

ALTER TABLE Users
ADD CONSTRAINT df_IsSendEmailInvite
DEFAULT 0 FOR IsSendEmailInvite

ALTER TABLE Users
ADD CONSTRAINT df_IsActive
DEFAULT 0 FOR IsActive

ALTER TABLE Users
ADD CONSTRAINT df_IsDeleted
DEFAULT 0 FOR IsDeleted

