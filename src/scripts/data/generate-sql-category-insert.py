f = open('categories.txt', 'r')
set_idendity_on = 'SET IDENTITY_INSERT [tb_Focus] ON;'
set_idendity_off = 'SET IDENTITY_INSERT [tb_Focus] OFF;'
insert = 'insert into tb_Focus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description]) values (<<id>>, NEWID(), GETDATE(), 0, 0, \'<<desc>>\');'

script = ''

for i, v in enumerate(f):
    stripped = str(v).strip('\n').strip('\t')
    if(stripped):
        if(i == 0):
            script += set_idendity_on + '\n'

        populated_insert = insert.replace('<<id>>', str(i + 1)).replace('<<desc>>', stripped) + '\n'
        script += populated_insert
        #print(i, populated_insert)

script += set_idendity_off
print(script)

