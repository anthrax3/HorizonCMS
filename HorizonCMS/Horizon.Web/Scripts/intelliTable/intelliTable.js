$.fn.intelliTable = function (options) {

    /* Default parameters*/
    var defaults = {
        loaderSelector: "",
        token: "",
        cols: [],
        page: 1,
        records: 10,
        entity: 'Notifications',
        services: {
            single: 'xxx/xxx',
            create: 'xxx/xxx',
            retrieve: 'xxx/xxx',
            update: 'xxx/xxx',
            remove: 'xxx/xxx'
        },
        orderBy: 'CreateDate',
        orderDirection: 'Asc',
        showNavigation: true,
        showControls: true
    };

    /* Settings */
    var settings = $.extend({}, defaults, options);

    return this.each(function () {

        var table = $(this);
        var tableHeader = $(table).find('thead');
        var tableBody = $(table).find('tbody');
        var tableFooter = $(table).find('tfoot');
        var loader = $(options.loaderSelector);
        /*
        * EVENTS
        */
        $(this)
           /* table initalizes */
           .on('init', function (event) {
               /* trigger loading status*/
               $(this).trigger('loading');

               getData(true);


           })
           // table is loading data
           .on('loading', function () {
               /* establish loading animation */
               $(loader).css({ opacity: 1 }, 500);

           })
           // table finished loading data
           .on('loaded', function () {
               /* remove loading animation */
               $(loader).css({ opacity: 0 }, 2500);
               /* show table data */
           });

        /* DATA FUNCTIONS
        *
        */
        var createData = function (obj) {
            $.ajax({
                url: options.services.create,
                async: true,
                contentType: 'application/json; charset=utf-8',
                headers: { "__RequestVerificationToken": options.token },
                data: JSON.stringify(obj),
                type: 'POST',
                datatype: 'json'
            }).success(function (result) {
                if (result != null) {
                    $('#createModal').modal('hide');
                    getData(true, true);


                }
            }).error(function (xhr, status) { debugger; });
        };

        var updateData = function (obj) {
            $.ajax({
                url: options.services.update,
                async: true,
                contentType: 'application/json; charset=utf-8',
                headers: { "__RequestVerificationToken": options.token },
                data: JSON.stringify(obj),
                type: 'POST',
                datatype: 'json'
            }).success(function (result) {
                if (result != null) {
                    $('#updateModal').modal('hide');
                    getData(true, true);


                }
            }).error(function (xhr, status) { debugger; });
        };

        var deleteData = function (id) {
            $.ajax({
                url: options.services.remove,
                async: true,
                contentType: 'application/json; charset=utf-8',
                headers: { "__RequestVerificationToken": options.token },
                data: JSON.stringify({ notificationsId: id }),
                type: 'POST',
                datatype: 'json'
            }).success(function (result) {
                if (result != null) {
                    $('#removeModal').modal('hide');
                    getData(true, true);


                }
            }).error(function (xhr, status) { debugger; });
        };

        /*
        * GET DATA
        */
        var getData = function (async, forceRenderFooter) {
            debugger;
            $(table).trigger('loading');
            $.ajax({
                url: options.services.retrieve,
                async: true,
                contentType: 'application/json; charset=utf-8',
                headers: { "__RequestVerificationToken": options.token },
                data: JSON.stringify({ page: options.page, records: options.records, orderBy: options.orderBy, orderDirection: options.orderDirection }),
                type: 'POST',
                datatype: 'json'
            }).success(function (result) {
                if (result != null) {

                    // RENDER HEADER
                    if ($(tableHeader).find('tr').length === 0)
                        RenderHeader(options.cols);

                    // Render body
                    RenderBody(result[options.entity]);

                    // RENDER FOOTER
                    if ($(tableFooter).find('ul').length === 0 || forceRenderFooter === true)
                        RenderFooter(options.page, options.records, result.Count);

                    $(tableBody).find('input[type="checkbox"]').on('click', function ()
                    { toggleActionMenu() });
                    $(table).trigger('loaded');
                }
            }).error(function (xhr, status) { debugger; });
        };

        /* RENDER FUNCTIONS
        *  comments.
        */

        var RenderTable = function (data) {
            RenderHeader(options.cols);
            if (data != null) {
                RenderBody(data);

                RenderFooter(options.page, options.records, data.Count);

            }
        };

        /* RENDER HEADER
        *
        */
        var RenderHeader = function (columns) {
            var header = '<tr>';

            for (var i = 0; i < columns.length; i++) {
                header += '<th>';

                if (columns[i].searchable === false && columns[i].sortable === false) {
                    header += '<label>' + columns[i].name + '</label>';
                }
                else {
                    header += '<div class="input-group input-group-sm"><label class="input-group-addon">' + columns[i].name + '</label>';

                    if (columns[i].searchable === true) {
                        header += '<input type="text" class="form-control" placeholder="Username" aria-describedby="basic-addon1"/>';
                    }

                    if (columns[i].sortable === true) {
                        header += '<span class="input-group-btn"><button type="button" class="btn btn-sm btn-default sort-button" data-sortfield="' + columns[i].name + '" data-sortdirection="Asc"><span class="glyphicon glyphicon-sort"></span></button></span>';
                    }

                }

                header += '</div></th>';

            }
            header += '</tr>';

            $(tableHeader).append(header);

            $(tableHeader).find('.sort-button').on('click', function () {

                options.orderBy = $(this).data('sortfield');
                options.orderDirection = $(this).data('sortdirection');

                if ($(this).data('sortdirection') === "Asc")
                    $(this).data('sortdirection', 'Desc');
                else
                    $(this).data('sortdirection', 'Asc');

                getData(true);

            });
        };

        /* RENDER BODY
        *
        */
        var RenderBody = function (data) {

            $(tableBody).html('');

            for (var i = 0; i < data.length; i++) {

                var row = '<tr>';

                for (var columns = 0; columns < options.cols.length; columns++) {
                    row += '<td>';
                    if (options.cols[columns].principal === true) {
                        row += '<input class="select" type="checkbox" name="' + options.cols[columns].name + '" id="' + options.cols[columns].name + '" value="' + data[i][options.cols[columns].name] + '" />';
                    }
                    else {

                        if (options.cols[columns].type === 'date') {

                            var d = eval("new " + data[i][options.cols[columns].name].replace(/[\\/]/g, ""));
                            row += d.toLocaleDateString();
                        }
                        else {
                            row += data[i][options.cols[columns].name];
                        }
                    }
                    row += '</td>'
                }
                row += '</tr>';

                $(tableBody).append(row);


            }


        };

        /*
       * SHOW ACTION MENU
       */
        var toggleActionMenu = function () {
            /*
           * ATTACH FUNCTIONALITY
           */

            var selectedItems = $(tableBody).find('input[type="checkbox"]:checked');

            if (selectedItems.length > 1) {
                $(tableFooter).find('#btnShowDelete').prop('disabled', false);
                $(tableFooter).find('#btnEdit').prop('disabled', true);
            }
            else if (selectedItems.length == 1) {
                $(tableFooter).find('#btnShowDelete').prop('disabled', false);
                $(tableFooter).find('#btnEdit').prop('disabled', false);
            }
            else {
                $(tableFooter).find('#btnShowDelete').prop('disabled', true);
                $(tableFooter).find('#btnEdit').prop('disabled', true);
            }

        };

        /* RENDER
        *
        */
        var RenderFooter = function (page, records, total) {

            $(tableFooter).html('');
            // Render Footer
            var footer = '<tr><td colspan="' + options.cols.length + '">';

            if (options.showNavigation === true) {
                footer += '<ul class="pagination">';
                var pageCount = Math.ceil(total / records);
                for (var i = 0; i < pageCount; i++) {
                    if (i === page - 1)
                        footer += '<li class="active"><a class="navigation" data-val="' + (i + 1) + '" href="#">' + (i + 1) + '</a></li>';
                    else
                        footer += '<li><a class="navigation" data-val="' + (i + 1) + '" href="#">' + (i + 1) + '</a></li>';
                }
                footer += '</ul>';
            }

            if (options.showControls === true)
                footer += '<div class="pull-right"><button id="btnCreate" type="button" class="btn btn-success"><span class="glyphicon glyphicon-plus"></span> Create</button> <button id="btnShowDelete" type="button" class="btn btn-danger" disabled><span class="glyphicon glyphicon-remove"></span> Delete</button> <button id="btnEdit" type="button" class="btn btn-primary" disabled><span class="glyphicon glyphicon-edit"></span> Edit</button></div>';

            footer += '</td></tr>';

            $(tableFooter).append(footer);

            $(tableFooter).find('li a.navigation').on('click', function (e) {
                e.preventDefault();
                $(tableFooter).find('li').removeClass('active');
                $(this).parent().toggleClass('active');
                gotoPage($(this).data('val'));
            });

            $(tableFooter).find('#btnCreate').on('click', function () {
                $('#createModal #acceptButton').on('click', function () {
                    var data = $('#createModal .modal-body form').serializeObject();
                    createData(data);

                });
                $('#createModal').modal('show');

            });

            $(tableFooter).find('#btnEdit').on('click', function (event) {

                var itemId = $(table).find('input[type="checkbox"]:checked')[0].defaultValue;
                $.ajax({
                    url: options.services.single,
                    async: true,
                    contentType: 'application/json; charset=utf-8',
                    headers: { "__RequestVerificationToken": options.token },
                    data: JSON.stringify({ id: itemId }),
                    type: 'POST',
                    datatype: 'json'
                }).success(function (result) {
                    if (result != null) {

                        var data = result[options.entity];

                        for (var i = 0; i < options.cols.length; i++) {
                            if (options.cols[i].type === "string") {

                                if ($('#updateModal .modal-body').find('#' + options.cols[i].name)[0].children.length > 0) {
                                    $('#updateModal .modal-body').find('#' + options.cols[i].name + ' option[value="' + data[options.cols[i].name] + '"]').attr('selected', true);
                                }
                                else {
                                    $('#updateModal .modal-body').find('#' + options.cols[i].name).val(data[options.cols[i].name]);
                                }
                            }
                            else if (options.cols[i].type === "bool") {
                                if ($('#updateModal .modal-body').find('#' + options.cols[i].name).attr('type') === "select") {
                                    $('#updateModal .modal-body').find('#' + options.cols[i].name + ' option[value="' + data[options.cols[i].name] + '"]').attr("selected", true);
                                }
                                else {
                                    $('#updateModal .modal-body').find('#' + options.cols[i].name).val(data[options.cols[i].name]);
                                }
                            }
                        }

                        $('#updateModal #acceptButton').on('click', function () {

                            var data = $('#updateModal .modal-body form').serializeObject();
                            updateData(data)
                        });

                        $('#updateModal').modal('show');

                    }
                });

            });
            $(tableFooter).find('#btnShowDelete').on('click', function () {

                var checkedIds = $(tableBody).find('input[type="checkbox"]:checked');
                var ids = new Array();

                for (var i = 0; i < checkedIds.length; i++) {
                    ids.push(checkedIds[i].value);
                }

                $('#removeModal .modal-body').html('<p>You are going to delete some elements</p>');
                $('#removeModal #parameters').val(ids);
                $('#removeModal #acceptButton').on('click', function () {

                    deleteData(ids)
                });
                $('#removeModal').modal('show');


            });


        };

        /*
        * NAVIGATION FUNCTIONS
        */

        var gotoPage = function (page) {
            if (page > 0) {
                options.page = page;
                $(table).find('#btnDelete').prop('disabled', true);
                $(table).find('#btnEdit').prop('disabled', true);
                getData(true);

            }
        };




        $(this).trigger('init');
    });
};