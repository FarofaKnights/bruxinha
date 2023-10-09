using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caldeirao : AcaoBehaviour {
    public SpriteRenderer visualizacao;
    public Animator visualizacaoAnimator;
    public InventarioLista ingredientes;

    Signo resultado = null;
    int quantidadeResultado = 0;

    public List<Receita> receitas;
    public Signo erroGenerico;

    public override void FazerAcao(bool leftClick) {
        if (leftClick) {
            if (resultado == null) ColocarIngrediente();
            else PegarResultado();
        } else {
            Ferver();
        }
    }

    public void ColocarIngrediente() {
        Slot slot = Player.instance.mao.GetAtual();
        if (slot == null) return;

        Signo signo = slot.item;
        slot.Subtract(1);
        ingredientes.Add(signo, 1);

        visualizacao.sprite = signo.sprite;
        visualizacaoAnimator.SetTrigger("DropItem");
    }

    public void Ferver() {
        List<Slot> slots = ingredientes.GetSlots();
        if (slots.Count == 0) return;

        Receita receita = null;
        foreach (Receita r in receitas) {
            if (r.ChecarReceita(slots)) {
                receita = r;
                break;
            }
        }

        if (receita == null) resultado = erroGenerico;
        else resultado = receita.resultado;

        quantidadeResultado = receita.quantidade;

        ingredientes.Clear();

        visualizacao.sprite = resultado.sprite;
        visualizacaoAnimator.SetTrigger("ShowItem");
    }

    public void PegarResultado() {
        visualizacaoAnimator.SetTrigger("PickupItem");

        Player.instance.mao.Add(resultado, quantidadeResultado);
        Player.instance.mao.Selecionar(resultado);
        
        resultado = null;
        quantidadeResultado = 0;
    }
}
