var OpenDocs = {
    sync: function (model) {
        var temp = {
            Id: model.Id,
            Revision: model.Revision,
            Title: model.getTitle(),
            Content: model.getContent()
        };

        // Local change made since last sync, increment revision
        if (model.getContent() !== model.LastContent) {
            temp.Revision++;
        }

        $.ajax({
            url: '/documents/' + model.Id,
            method: 'post',
            dataType: 'json',
            data: temp
        }).done(function (server) {
            // Server copy is newer, use that as the primary text
            if (model.Revision < server.Revision) {
                text1 = model.getContent();
                text2 = server.Content;

                var dmp = new diff_match_patch();

                // Compute patch required between the old and new text
                var patches = dmp.patch_make(text1, text2);

                // Apply patch to old text
                var result = dmp.patch_apply(patches, text1)[0];

                model.Revision = server.Revision;
                model.setContent(result);
                model.LastContent = model.getContent();
            }
        });
    }
};