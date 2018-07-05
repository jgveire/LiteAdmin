<template>
    <div class="edit">
        <md-card class="md-layout-item md-small-size-100 md-medium-size-100 md-large-size-66 md-xlarge-size-50">
            <md-card-header>
                <div class="md-title">Edit {{getFriendlyName(tableName)}}</div>
            </md-card-header>

            <md-card-content>
                <div v-for="(column, index) in tableSchema.columns"
                     :key="column.name">
                    <md-checkbox v-if="column.dataType == 'Boolean'"
                                 :id="column.name"
                                 :name="column.name"
                                 :ref="column.name"
                                 v-model="item[column.name]">{{getFriendlyName(column.name)}}</md-checkbox>
                    <md-datepicker v-else-if="column.dataType == 'DateTime'"
                                    :id="column.name"
                                    :name="column.name"
                                    :ref="column.name"
                                    v-model="item[column.name]"
                                    :maxlength="getMaxLength(column.maxLength)"
                                    :disabled="column.isPrimaryKey"
                                    :md-open-on-focus="false">
                        <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                    </md-datepicker>
                    <md-field v-else>
                        <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                        <md-input :id="column.name"
                                  :name="column.name"
                                  :ref="column.name"
                                  v-model="item[column.name]"
                                  :maxlength="getMaxLength(column.maxLength)"
                                  :disabled="column.isPrimaryKey"></md-input>
                    </md-field>

                </div>
            </md-card-content>

            <md-card-actions>
                <md-button v-on:click="save" class="md-raised md-accent">Save</md-button>
                <md-button v-on:click="cancel" class="md-raised">Cancel</md-button>
            </md-card-actions>
        </md-card>
    </div>
</template>

<script lang="ts" src="./Edit.ts"></script>
