$("#loginForm").submit(function (event) {
  event.preventDefault();
  let email = $("#email").val();
  let password = $("#password").val();

  $.ajax({
    url: "http://localhost:5195/restaurante/login",
    type: "POST",
    contentType: "application/json",
    data: JSON.stringify({ email: email, senha: password }),
    success: function (response) {
      // Armazena os dados no localStorage
      localStorage.setItem("token", response.Bearer);
      localStorage.setItem("nivelAcesso", response.NivelAcesso);
      localStorage.setItem("nomeUsuario", response.NomeUsuario);

      console.log("Token:", response.Bearer);
      console.log("NivelAcesso:", response.NivelAcesso);
      console.log("NomeUsuario:", response.NomeUsuario);

      // Redireciona o usuário com base no nível de acesso
      redirecionarUsuario(response.NivelAcesso);
    },
    error: function () {
      alert("Login falhou! Verifique suas credenciais.");
    },
  });
});
