(function () {
	$(function () {
		$('#input_apiKey').off();
		$('#input_apiKey').attr("placeholder", "Insert Bearer token here");

		$('#input_apiKey').on('change', function () {
			var token = this.value;
			if (token && token.trim() !== '') {
				swaggerUi.api.clientAuthorizations.add("bearer", new SwaggerClient.ApiKeyAuthorization("Authorization", "Bearer " + token, "header"));
				//console.log("Bearer JWT token authorization added: token = " + token);
			}
		});
	});
})();