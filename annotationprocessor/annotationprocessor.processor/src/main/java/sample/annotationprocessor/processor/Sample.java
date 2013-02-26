/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.annotationprocessor.processor;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 *
 * @author Kazuki
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface Sample {
    
}
