(function () {
	$(function () {
		$('#input_apiKey').off();
		$('#input_apiKey').attr("placeholder", "Bearer token here");

		$('#input_apiKey').on('change', function () {
			var key = this.value;
			if (key && key.trim() !== '') {
				swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("Authorization", "Bearer " + key, "header"));
				console.log("Bearer authorization added: token = " + apiKey);
			}
		});
	});
})();