// symbole " $ ", permet de récuperer un élément du html

$(".option").hover(function ()
{
    $(".option").removeClass("active");
    $(this).addClass("active");
});

// Sur le click de l'image, redirige pour lancer la commande ! :) 
// On est heureux avec Zack pour ce projet BEEEERZ !
function RedirectToDetails(id)
{
    window.location.href = $("#AddCommande").val().replace('__id__', id);
};


