f = open('timezones.txt', 'r')
set_idendity_on = 'SET IDENTITY_INSERT [tb_Timezone] ON;'
set_idendity_off = 'SET IDENTITY_INSERT [tb_Timezone] OFF;'
insert = 'insert into tb_Timezone(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], [UtcOffset], [ExtCode]) values (<<id>>, NEWID(), GETDATE(), 0, 0, \'<<display>>\', <<utcoffset>>, \'<<extcode>>\');'

script = ''

for i, v in enumerate(f):
    stripped = str(v).strip('\n')

    if(stripped):
        if(i == 0):
            script += set_idendity_on + '\n'

        split = stripped.split('\t')

        ext_code = split[0]
        split = split[1].split('|')
        display = split[0].replace('\t', ' ')

        utc_offset = 0
        if(len(split) == 2):
            utc_offset = int(split[1])

        if(utc_offset != 0):
            populated_insert = insert.replace('<<id>>', str(i + 1)).replace('<<display>>', display).replace('<<extcode>>', ext_code).replace('<<utcoffset>>', str(utc_offset)) + '\n'
            script += populated_insert

script += set_idendity_off
print(script)

