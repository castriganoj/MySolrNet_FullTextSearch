var filterController = {
    init:function(){
        FilterVeiw.init();
    }
}

var FilterVeiw = {
    
    init: function () {

        this.searchForm = $('#searchForm');
        this.DocFacetChecks = $('#documentTypeFacet');
        this.StatusFacetChecks = $('#statusFacet');
        this.dateFacet = $('#dateFacet');
        this.orgfacet = $('#orgLocationFacet');
       
        this.parentBoxes = $('#filters .parentFilter') 

        //this.HTMLClearParentFilter = $('<span>Clear Filters</span>')
        //this.HTMLClearAllFilters = $('<span>Clear All Filters</span>')
        this.filterCheckBoxes = $('#filters :checkbox');
        this.filtersArea = $('#filters')

        this.render();

    },

    render: function () {

        var checkBoxes = this.filterCheckBoxes;

        for (var i = 0; i < checkBoxes.length; i++) {

            check = checkBoxes[i]
            var searchForm = this.searchForm;

            //$(check).change(function(){
                
            //    searchForm.submit();
            //})
        };

      
        //check the parent box if a child box is selected
        for (var i = 0; i < this.parentBoxes.length; i++) {
            var filterParent = this.parentBoxes[i];
            var HTMLClearFilters = $('<a class = "clearFilters">Clear Filters</a>')
            $(filterParent).prepend(HTMLClearFilters)
        }

        $('.clearFilters').click(function(){
            var childFilters = $(this.parentNode).find('input');
            childFilters.prop('checked', false);
        });

        var clearAllFilters = $('<a id = "clearAllFilters">Clear ALL Filters</a>')
        $(this.filtersArea).prepend(clearAllFilters)

        $(clearAllFilters).click(function () {
            var childFilters = $(this.parentNode).find('input');
            childFilters.prop('checked', false);
        })

        $('.searchResultCell').click(function () {
            $(this).find('#MoreData').show();
        })

    }

   
}

$(document).ready(function () {
    filterController.init();
})