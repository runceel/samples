package com.mycompany.primesample;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import javax.annotation.PostConstruct;
import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import org.apache.commons.lang.StringUtils;

@ManagedBean
@ViewScoped
public class JavaScriptRCPPage implements Serializable {

    private static final long serialVersionUID = 1L;
    private List<Person> people = new ArrayList<>();

    public void addPersonJS() {
        Map<String, String> params = FacesContext
                .getCurrentInstance()
                .getExternalContext()
                .getRequestParameterMap();

        this.addPerson(params.get("id"), params.get("name"));
    }

    public void addPerson(String id, String name) {
        if (StringUtils.isEmpty(id)) {
            this.addMessage(FacesMessage.SEVERITY_ERROR, "IDを入力してください");
            return;
        }
        
        if (!StringUtils.isNumeric(id)) {
            this.addMessage(FacesMessage.SEVERITY_ERROR, "IDは数字で入力してください");
            return;
        }
        
        if (StringUtils.isEmpty(name)) {
            this.addMessage(FacesMessage.SEVERITY_ERROR, "名前を入力してください");
            return;
        }
        
        Person p = new Person();
        p.setId(Integer.parseInt(id));
        p.setName(name);
        this.people.add(p);
    }

    private void addMessage(FacesMessage.Severity severity, String message) {
        FacesContext.getCurrentInstance()
                .addMessage(null, new FacesMessage(severity, message, null));
    }

    public List<Person> getPeople() {
        return people;
    }

    public void setPeople(List<Person> people) {
        this.people = people;
    }
}
