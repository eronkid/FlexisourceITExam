$.get("/Api/Files", function (data) {
    var i = 0;
    $(data).each(function (k1, v1) {
        var divSelector = $('#clientList > div').eq(i);
        divSelector.find('.clientId').text('(' + v1.Id + ')');

        $(v1.SortedFiles).each(function (k2, v2) {
            divSelector.find('.customOrdered').append('<li><strong>' + v2 + '</strong></li>');
        });

        $(v1.UnorderedFiles).each(function (k2, v2) {
            divSelector.find('.unordered').append('<li>' + v2 + '</li>');
        });

        i++;
    });
});