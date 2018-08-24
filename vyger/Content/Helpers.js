function setupExercisePicker(queryUrl, $groupId, $categoryId, $exerciseId)
{
    $groupId.change(function ()
    {
        loadExercises();
    });

    $categoryId.change(function ()
    {
        loadExercises();
    });

    loadExercises();

    function loadExercises()
    {
        var groupId = $groupId.val();
        var categoryId = $categoryId.val();

        var url = queryUrl + '?groupId=' + groupId + '&categoryId=' + categoryId;

        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            success: function (exercises)
            {
                $exerciseId.find('option').remove();

                $('<option/>')
                    .attr('value', '')
                    .text('(exercise)')
                    .appendTo($exerciseId);

                $.each(exercises, function (i, exercise)
                {
                    $('<option/>')
                        .attr('value', exercise.Id)
                        .text(exercise.DetailName)
                        .appendTo($exerciseId);
                });
            }
        });
    }
}