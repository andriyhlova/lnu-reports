(function () {
    (function () {
        const selectedDegrees = [];
        let allDegrees = [];
        $(function () {
            getDegrees();
            getSelectedDegrees();
            const degreeEntityComponent = new RelatedEntityComponent(getSettings());
            degreeEntityComponent.load();
        });

        function getSettings() {
            return {
                relatedEntityContainerId: '#degree-related-entity',
                selectedItems: selectedDegrees,
                getFormHtml: getDegreeFormHtml,
                postLoadForm: updateDegreeList,
                getRelatedEntityFormObject: getDegreeFormObject,
                getSelectedRelatedEntityHtml: getSelectedDegreeHtml
            };
        }

        function getDegrees() {
            $.ajax('/api/degreesapi/getAll')
                .done(function (degrees) {
                    allDegrees = degrees;
                    updateDegreeList();
                });
        }

        function getSelectedDegrees() {
            const degrees = $('#degree-related-entity .selected-item');
            for (let i = 0; i < degrees.length; i++) {
                const element = $(degrees[i]);
                const degree = {
                    Id: element.find('.id').val(),
                    DegreeId: element.find('.degreeId').val(),
                    DegreeName: element.find('.degreeName').val(),
                    AwardYear: element.find('.awardYear').val(),
                }

                selectedDegrees.push(degree);
            }
        }

        function updateDegreeList() {
            const degreeElement = $("#degree-selector");
            if (!degreeElement.length || degreeElement.children().length) {
                return;
            }

            let str = "<option value=''>Виберіть науковий ступінь</option>";
            for (var i = 0; i < allDegrees.length; i++) {
                let degree = allDegrees[i];
                str += `<option value='${degree.Id}'>${degree.Value}</option>`;
            }

            degreeElement.html(str);
            degreeElement.chosen();
        }

        function getDegreeFormObject() {
            const degreeId = $('#degree-related-entity .new-related-entity-form select').val();
            if (!degreeId) {
                alert('Виберіть науковий ступінь');
                return;
            }

            const degreeName = $('#degree-related-entity .new-related-entity-form select :selected').text();

            const awardYear = $('#degree-related-entity .new-related-entity-form input[name=awardYear]').val();
            if (!validateYear(awardYear)) {
                alert('Введіть коректний рік присвоєння');
                return;
            }

            return {
                Id: 0,
                AwardYear: awardYear,
                DegreeId: degreeId,
                DegreeName: degreeName
            };
        }

        function getDegreeFormHtml() {
            return `<div>
                        <label class="control-label">Науковий ступінь <span class="text-danger">*</span></label>
                        <div><select id="degree-selector" class="form-control chosen-select"></select></div>
                    </div>
                    <div>
                        <label class="control-label">Рік присвоєння<span class="text-danger">*</span></label>
                        <input class="form-control" type="number" name="awardYear" value="" min="${minYear}" max="${maxYear}"/>
                    </div>`;
        }

        function getSelectedDegreeHtml(index, degree) {
            return `<div>
                    <div class="selected-item">
                            <div>
                                <div>${degree.DegreeName}</div>
                                <div>Рік присвоєння: ${degree.AwardYear}</div>
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="Degrees[${index}].Id" class="id" value="${degree.Id}" />
                            <input type="hidden" name="Degrees[${index}].DegreeId" class="degreeId" value="${degree.DegreeId}" />
                            <input type="hidden" name="Degrees[${index}].DegreeName" class="degree" value="${degree.DegreeName}" />
                            <input type="hidden" name="Degrees[${index}].AwardYear" class="awardYear" value="${degree.AwardYear}" />
                    </div>
                </div>`;
        }
    }());

    (function () {
        const selectedAcademicStatuses = [];
        let allAcademicStatuses = [];
        $(function () {
            getAcademicStatuses();
            getSelectedAcademicStatuses();
            let academicStatusEntityComponent = new RelatedEntityComponent(getSettings());
            academicStatusEntityComponent.load();
        });

        function getSettings() {
            return {
                relatedEntityContainerId: '#academic-status-related-entity',
                selectedItems: selectedAcademicStatuses,
                getFormHtml: getAcademicStatusFormHtml,
                postLoadForm: updateAcademicStatusList,
                getRelatedEntityFormObject: getAcademicStatusFormObject,
                getSelectedRelatedEntityHtml: getSelectedAcademicStatusHtml
            };
        }

        function getAcademicStatuses() {
            $.ajax('/api/academicStatuses/getAll')
                .done(function (academicStatuses) {
                    allAcademicStatuses = academicStatuses;
                    updateAcademicStatusList();
                });
        }

        function getSelectedAcademicStatuses() {
            const academicStatuses = $('#academic-status-related-entity .selected-item');
            for (let i = 0; i < academicStatuses.length; i++) {
                const element = $(academicStatuses[i]);
                const academicStatus = {
                    Id: element.find('.id').val(),
                    AcademicStatusId: element.find('.academicStatusId').val(),
                    AcademicStatusName: element.find('.academicStatusName').val(),
                    AwardYear: element.find('.awardYear').val(),
                }

                selectedAcademicStatuses.push(academicStatus);
            }
        }

        function updateAcademicStatusList() {
            const academisStatusElement = $("#academic-status-selector");
            if (!academisStatusElement.length || academisStatusElement.children().length) {
                return;
            }

            let str = "<option value=''>Виберіть вчене звання</option>";
            for (var i = 0; i < allAcademicStatuses.length; i++) {
                let academicStatus = allAcademicStatuses[i];
                str += `<option value='${academicStatus.Id}'>${academicStatus.Value}</option>`;
            }

            academisStatusElement.html(str);
            academisStatusElement.chosen();
        }

        function getAcademicStatusFormObject() {
            const academicStatusId = $('#academic-status-related-entity .new-related-entity-form select').val();
            if (!academicStatusId) {
                alert('Виберіть вчене звання');
                return;
            }

            const academicStatusName = $('#academic-status-related-entity .new-related-entity-form select :selected').text();

            const awardYear = $('#academic-status-related-entity .new-related-entity-form input[name=awardYear]').val();
            if (!validateYear(awardYear)) {
                alert('Введіть коректний рік присвоєння');
                return;
            }

            return {
                Id: 0,
                AwardYear: awardYear,
                AcademicStatusId: academicStatusId,
                AcademicStatusName: academicStatusName
            };
        }

        function getAcademicStatusFormHtml() {
            return `<div>
                        <label class="control-label">Вчене звання <span class="text-danger">*</span></label>
                        <div><select id="academic-status-selector" class="form-control chosen-select"></select></div>
                    </div>
                    <div>
                        <label class="control-label">Рік присвоєння<span class="text-danger">*</span></label>
                        <input class="form-control" type="number" name="awardYear" value="" min="${minYear} max="${maxYear}"/>
                    </div>`;
        }

        function getSelectedAcademicStatusHtml(index, academicStatus) {
            return `<div>
                    <div class="selected-item">
                            <div>
                                <div>${academicStatus.AcademicStatusName}</div>
                                <div>Рік присудження: ${academicStatus.AwardYear}</div>
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="AcademicStatuses[${index}].Id" class="id" value="${academicStatus.Id}" />
                            <input type="hidden" name="AcademicStatuses[${index}].AcademicStatusId" class="academicStatusId" value="${academicStatus.AcademicStatusId}" />
                            <input type="hidden" name="AcademicStatuses[${index}].AcademicStatusName" class="academicStatusName" value="${academicStatus.AcademicStatusName}" />
                            <input type="hidden" name="AcademicStatuses[${index}].AwardYear" class="awardYear" value="${academicStatus.AwardYear}" />
                    </div>
                </div>`;
        }
    }());

    (function () {
        const selectedHonoraryTitles = [];
        let allHonoraryTitles = [];
        $(function () {
            getHonoraryTitles();
            getSelectedHonoraryTitles();
            let honoraryTitleEntityComponent = new RelatedEntityComponent(getSettings());
            honoraryTitleEntityComponent.load();
        });

        function getSettings() {
            return {
                relatedEntityContainerId: '#honorary-title-related-entity',
                selectedItems: selectedHonoraryTitles,
                getFormHtml: getHonoraryTitleFormHtml,
                postLoadForm: updateHonoraryTitleList,
                getRelatedEntityFormObject: getHonoraryTitleFormObject,
                getSelectedRelatedEntityHtml: getSelectedHonoraryTitleHtml
            };
        }

        function getHonoraryTitles() {
            $.ajax('/api/honoraryTitles/getAll')
                .done(function (honoraryTitles) {
                    allHonoraryTitles = honoraryTitles;
                    updateHonoraryTitleList();
                });
        }

        function getSelectedHonoraryTitles() {
            const honoraryTitles = $('#honorary-title-related-entity .selected-item');
            for (let i = 0; i < honoraryTitles.length; i++) {
                const element = $(honoraryTitles[i]);
                const honoraryTitle = {
                    Id: element.find('.id').val(),
                    Value: element.find('.value').val()
                }

                selectedHonoraryTitles.push(honoraryTitle);
            }
        }

        function updateHonoraryTitleList() {
            const honoraryTitleElement = $("#honorary-title-selector");
            if (!honoraryTitleElement.length || honoraryTitleElement.children().length) {
                return;
            }

            let str = "<option value=''>Виберіть почесне звання</option>";
            for (var i = 0; i < allHonoraryTitles.length; i++) {
                let honoraryTitle = allHonoraryTitles[i];
                str += `<option value='${honoraryTitle.Id}'>${honoraryTitle.Value}</option>`;
            }

            honoraryTitleElement.html(str);
            honoraryTitleElement.chosen();
        }

        function getHonoraryTitleFormObject() {
            const honoraryTitleId = $('#honorary-title-related-entity .new-related-entity-form select').val();
            if (!honoraryTitleId) {
                alert('Виберіть почесне звання');
                return;
            }

            const honoraryTitleValue = $('#honorary-title-related-entity .new-related-entity-form select :selected').text();

            return {
                Id: honoraryTitleId,
                Value: honoraryTitleValue
            };
        }

        function getHonoraryTitleFormHtml() {
            return `<div>
                        <label class="control-label">Почесне звання <span class="text-danger">*</span></label>
                        <div><select id="honorary-title-selector" class="form-control chosen-select"></select></div>
                    </div>`;
        }

        function getSelectedHonoraryTitleHtml(index, honoraryTitle) {
            return `<div>
                    <div class="selected-item">
                            <div>
                                ${honoraryTitle.Value}
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="HonoraryTitles[${index}].Id" class="id" value="${honoraryTitle.Id}" />
                            <input type="hidden" name="HonoraryTitles[${index}].Value" class="value" value="${honoraryTitle.Value}" />
                    </div>
                </div>`;
        }
    }());

    const minYear = 1950;
    const maxYear = new Date().getFullYear();
    function validateYear(value) {
        return value && +value >= minYear && +value <= maxYear;
    }
}());