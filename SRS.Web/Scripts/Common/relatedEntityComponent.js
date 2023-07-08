class RelatedEntityComponent {
    _relatedEntityContainerId;
    _selectedItems;
    _getFormHtml;
    _postLoadForm;
    _getRelatedEntityFormObject;
    _getSelectedRelatedEntityHtml;
    _identifierClass = '.id';
    _identifierProperty = 'Id';

    constructor(settings) {
        this._relatedEntityContainerId = settings.relatedEntityContainerId;
        this._selectedItems = settings.selectedItems;
        this._getFormHtml = settings.getFormHtml;
        this._postLoadForm = settings.postLoadForm;
        this._getRelatedEntityFormObject = settings.getRelatedEntityFormObject;
        this._getSelectedRelatedEntityHtml = settings.getSelectedRelatedEntityHtml;
        this._identifierClass = settings.identifierClass;
        this._identifierProperty = settings.identifierProperty;
    }

    load() {
        this.addRelatedEntityEvent();
        this.cancelDegreeEvent();
        this.removeRelatedEntityEvent();
        this.saveRelatedEntityEvent();
    }

    addRelatedEntityEvent() {
        $(`${this._relatedEntityContainerId} .add-related-entity`).click((e) => {
            const form = $(`${this._relatedEntityContainerId} .new-related-entity`);
            if (form.children().length == 0) {
                form.html(this.renderFormHtml());
                if (this._postLoadForm) {
                    this._postLoadForm();
                }
            }
        });
    }

    cancelDegreeEvent() {
        $(this._relatedEntityContainerId).on('click', '.cancel-related-entity', () => {
            $(`${this._relatedEntityContainerId} .new-related-entity`).html('');
        });
    }

    removeRelatedEntityEvent() {
        $(this._relatedEntityContainerId).on('click', '.bi-file-x-fill', (element) => {
            const container = $(element.currentTarget).closest('.selected-item');
            const id = container.find(this._identifierClass).val();
            const index = this._selectedItems.findIndex(x => x[this._identifierProperty] == id);
            if (index != -1) {
                container.remove();
                this._selectedItems.splice(index, 1);
                this.renderRelatedEntityList(this._selectedItems);
            }
        });
    }

    saveRelatedEntityEvent() {
        $(this._relatedEntityContainerId).on('click', '.save-related-entity', () => {
            const item = this._getRelatedEntityFormObject();
            if (!item) {
                return;
            }
            this._selectedItems.push(item);
            $(`${this._relatedEntityContainerId} .selected-items`).append(this._getSelectedRelatedEntityHtml(this._selectedItems.length - 1, item));
            $(`${this._relatedEntityContainerId} .new-related-entity`).html('');
        });
    }

    renderFormHtml() {
        return `<div class="new-related-entity-form">
                    ${this._getFormHtml()}
                    <div class="related-entity-buttons">
                        <button class="btn btn-danger cancel-related-entity" type="button">Відмінити</button>
                        <button class="btn btn-success save-related-entity" type="button">Зберегти</button>
                    </div>
                </div>`;
    }

    renderRelatedEntityList(entities) {
        const selectedItems = $(`${this._relatedEntityContainerId} .selected-items`);
        selectedItems.html('');
        for (let i = 0; i < entities.length; i++) {
            selectedItems.append(this._getSelectedRelatedEntityHtml(i, entities[i]));
        }
    };
}