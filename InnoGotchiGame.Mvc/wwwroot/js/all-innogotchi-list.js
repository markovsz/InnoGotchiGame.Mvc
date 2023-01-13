$(document).ready(function () {
    $('#order-by-age-button').click(function () {
        let urlParams = new URLSearchParams(window.location.search);
        let sortedByPrevVal = `SortedBy=${urlParams.get('SortedBy')}`;
        let sortedByNewVal = `SortedBy="age"`;
        let vals = window.location.search;
        if (urlParams.has('SortedBy'))
            vals = vals.replace(sortedByPrevVal, sortedByNewVal);
        else
            vals = vals.concat('&', sortedByNewVal);

        window.location.search = vals;
    });
    $('#order-by-hunger-button').click(function () {
        let urlParams = new URLSearchParams(window.location.search);
        let sortedByPrevVal = `SortedBy=${urlParams.get('SortedBy')}`;
        let sortedByNewVal = `SortedBy="hunger"`;
        let vals = window.location.search;
        if (urlParams.has('SortedBy'))
            vals = vals.replace(sortedByPrevVal, sortedByNewVal);
        else
            vals = vals.concat('&', sortedByNewVal);

        window.location.search = vals;
    });
    $('#order-by-thirst-button').click(function () {
        let urlParams = new URLSearchParams(window.location.search);
        let sortedByPrevVal = `SortedBy=${urlParams.get('SortedBy')}`;
        let sortedByNewVal = `SortedBy="thirst"`;
        let vals = window.location.search;
        if (urlParams.has('SortedBy'))
            vals = vals.replace(sortedByPrevVal, sortedByNewVal);
        else
            vals = vals.concat('&', sortedByNewVal);

        window.location.search = vals;
    });
});