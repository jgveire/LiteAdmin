<template>
    <div class="edit">
        <md-card class="md-layout-item md-small-size-100 md-medium-size-100 md-large-size-66 md-xlarge-size-50">
            <md-card-header>
                <div class="md-title">Edit {{getFriendlyName(tableName)}}</div>
            </md-card-header>

            <md-card-content>
                <div v-for="(column, index) in tableSchema.columns"
                     :key="column.name">
                    <md-checkbox v-if="column.dataType == 'Byte' || column.dataType == 'Boolean'"
                                 :id="column.name"
                                 :name="column.name"
                                 :ref="column.name"
                                 v-model="item[column.name]"
                                 :required="!column.isNullable">{{getFriendlyName(column.name)}}</md-checkbox>
                    <md-datepicker v-else-if="column.dataType == 'DateTime'"
                                   :id="column.name"
                                   :name="column.name"
                                   :ref="column.name"
                                   v-model="item[column.name]"
                                   :maxlength="getMaxLength(column.maxLength)"
                                   :disabled="column.isPrimaryKey"
                                   :md-open-on-focus="false"
                                   :required="!column.isNullable">
                        <label :for="column.name"
                               :class="column.isNullable ? '' : 'md-required'">
                            {{getFriendlyName(column.name)}}
                        </label>
                    </md-datepicker>
                    <md-field v-else-if="column.foreignKey">
                        <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                        <md-select id="column.name"
                                   :name="column.name"
                                   :ref="column.name"
                                   v-model="item[column.name]"
                                   :required="!column.isNullable">
                            <md-option value="">- empty -</md-option>
                            <md-option v-for="lookup in lookups[column.foreignTable]"
                                       :key="lookup.id"
                                       :value="lookup.id">{{lookup.name}}</md-option>
                        </md-select>
                    </md-field>
                    <md-field v-else-if="column.dataType == 'String' && (column.maxLength > 200 || column.maxLength <= 0)">
                        <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                        <md-textarea :id="column.name"
                                  :name="column.name"
                                  :ref="column.name"
                                  v-model="item[column.name]"
                                  :maxlength="getMaxLength(column.maxLength)"
                                  :disabled="column.isPrimaryKey"
                                  :required="!column.isNullable"></md-textarea>
                    </md-field>
                    <md-field v-else>
                        <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                        <md-input :id="column.name"
                                  :name="column.name"
                                  :ref="column.name"
                                  v-model="item[column.name]"
                                  :maxlength="getMaxLength(column.maxLength)"
                                  :disabled="column.isPrimaryKey"
                                  :required="!column.isNullable"></md-input>
                    </md-field>

                </div>
            </md-card-content>

            <md-card-actions>
                <md-button v-on:click="cancel" class="">Cancel</md-button>
                <md-button v-on:click="save" class="md-primary">Save</md-button>
            </md-card-actions>
        </md-card>
    </div>
</template>

<script lang="ts" src="./Edit.ts"></script>
<style lang="scss" src="./Edit.scss"></style>
