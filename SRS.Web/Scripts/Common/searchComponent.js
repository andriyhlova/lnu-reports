class SearchComponent {
    _searchContainerId;
    _searchUrl;
    _searchTimeOut;
    _getSearchResultText;
    _appendSearchResultItem;
    _results;
    constructor(searchContainerId, searchUrl, getSearchResultText, appendSearchResultItem) {
        this._searchContainerId = searchContainerId;
        this._searchUrl = searchUrl;
        this._getSearchResultText = getSearchResultText;
        this._appendSearchResultItem = appendSearchResultItem;
    }

    load() {
        this.addKeyUpEvent();
        this.addFocusEvent();
        this.addOutsideClickEvent();
        this.addItemClickEvent();
    }

    addKeyUpEvent() {
        $(`${this._searchContainerId}.search-container input`).keyup((e)=>{
            clearTimeout(this._searchTimeOut);
            if (e.target.value && e.target.value.length < 3) {
                return;
            }

            this._searchTimeOut = setTimeout(() => {
                $.ajax(this._searchUrl + `?search=${e.target.value}`)
                    .done((results) => {
                        this._results = results;
                        this.updateSearchResults(results);
                        this.toggleResults();
                    });
            }, 2000);
        });
    }

    addFocusEvent() {
        $(`${this._searchContainerId}.search-container input`).focus(() => {
            this.toggleResults();
        });
    }

    addOutsideClickEvent() {
        $(document).click((e) => {
            const container = $(`${this._searchContainerId}.search-container`)[0];
            if (container.contains(e.target)) {
                return false;
            }

            $(`${this._searchContainerId} .search-results`).hide();
        });
    }

    addItemClickEvent() {
        $(`${this._searchContainerId} .search-results`).on('click', '.search-item', (e) => {
            $(`${this._searchContainerId}.search-container input`).val('');
            this.updateSearchResults([]);
            $(`${this._searchContainerId} .search-results`).hide();
            const found = this._results.find(x => x.Id == e.currentTarget.dataset.id);
            this._appendSearchResultItem(found);
        });
    }

    updateSearchResults(results) {
        const searchResults = $(`${this._searchContainerId} .search-results`);
        searchResults.html('');
        for (let i = 0; i < results.length; i++) {
            const element = document.createElement('div');
            element.className = 'search-item';
            element.dataset.id = results[i].Id;
            element.innerHTML = this._getSearchResultText(results[i]);
            searchResults.append(element.outerHTML);
        }
    }

    toggleResults() {
        const results = $(`${this._searchContainerId} .search-results`);
        if (results.children().length) {
            results.show();
        }
        else {
            results.hide();
        }
    }
}